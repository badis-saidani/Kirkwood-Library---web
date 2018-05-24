using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace DataAccessLayer
{
    public static class StudentAccessor
    {
        public static int VerifyUsernameAndPassword(string username, string passwordHash)
        {
            var result = 0; // return the number of rows found (should be 1)

            // need to start with a connection
            var conn = DBConnection.GetDBConnection();

            // next, we need command text - a procedure name or query string
            var cmdText = @"sp_authenticate_student";

            // next, we use the connection and command text to create a command
            var cmd = new SqlCommand(cmdText, conn);

            // for a stored procedure, we need to set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // for a stored procedure, add any needed parameters
            cmd.Parameters.Add("@StudentEmail", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // set the parameter values
            cmd.Parameters["@StudentEmail"].Value = username;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;


            // now that the connection, command and parameters are set up,
            // we can execute the command

            // database code is always and everywhere unsafe code, so...
            try
            {
                // first, open the connection
                conn.Open();
                // then, execute the command
                result = (int)cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // good housekeeping requires connections to be closed
                conn.Close();
            }

            return result;
        }

        public static List<Role> RetrieveStudentRoles(string StudentID)
        {
            List<Role> roles = new List<Role>();

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_retrieve_student_roles";

            // create a command
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar, 20);

            // parameter values
            cmd.Parameters["@StudentID"].Value = StudentID;

            // try-catch
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var role = new Role()
                        {
                            RoleID = reader.GetString(0)
                        };
                        roles.Add(role);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return roles;
        }

        public static Student RetrieveStudentByUsername(string username)
        {
            Student student = null;

            // connection first
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_retrieve_student_by_email";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@StudentEmail", SqlDbType.NVarChar, 100);

            // parameeter values
            cmd.Parameters["@StudentEmail"].Value = username;

            // try-catch to execute the command
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    reader.Read(); // reads the next line in the result

                    // create a new Student object
                    student = new Student()
                    {
                        // SELECT 	[StudentID], [FirstName], [LastName], [PhoneNumber], [Email], [Active]
                        StudentID = reader.GetString(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        StudentEmail = reader.GetString(4),
                        Active = reader.GetBoolean(5)
                    };
                    if (student.Active != true)
                    {
                        throw new ApplicationException("Not an active Student.");
                    }
                }
                else
                {
                    throw new ApplicationException("Student record not found!");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }


            return student;
        }

        public static List<Student> RetrieveStudentByActive(bool active = true)
        {
            var studentList = new List<Student>();

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_retrieve_student_by_active";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters and values
            cmd.Parameters.AddWithValue("@Active", active);

            // try-catch
            try
            {
                // open connection
                conn.Open();
                // execute command
                var reader = cmd.ExecuteReader();

                // rows ?
                if (reader.HasRows)
                {
                    // process the reader
                    while (reader.Read())
                    {
                        var eq = new Student()
                        {
                            
                           StudentID = reader.GetString(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        StudentEmail = reader.GetString(4),
                        Active = reader.GetBoolean(5)
                        };
                        studentList.Add(eq);
                    }
                }
                else // no rows?
                {
                    throw new ApplicationException("Data not found.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database access error.", ex);
            }
            finally
            {
                conn.Close();
            }
            return studentList;
        }

        public static int DeactivateStudent(string studentID)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_student";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentID", studentID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return rows;
        }

        public static int DeleteStudent(string studentID)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_student";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentID", studentID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return rows;
        }

        public static List<Student> RetrieveStudentListByID(string stuidentID)
        {
            var studentList = new List<Student>();

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_retrieve_student_by_id";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters and values
            cmd.Parameters.AddWithValue("@StudentID", stuidentID);

            // try-catch
            try
            {
                // open connection
                conn.Open();
                // execute command
                var reader = cmd.ExecuteReader();

                // rows ?
                if (reader.HasRows)
                {
                    // process the reader
                    while (reader.Read())
                    {
                        var eq = new Student()
                        {

                            StudentID = reader.GetString(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            PhoneNumber = reader.GetString(3),
                            StudentEmail = reader.GetString(4),
                            Active = reader.GetBoolean(5)
                        };
                        studentList.Add(eq);
                    }
                }
                else // no rows?
                {
                    throw new ApplicationException("Data not found.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database access error.", ex);
            }
            finally
            {
                conn.Close();
            }
            return studentList;
        }

        public static List<Student> RetrieveStudentListByName(string name)
        {
            var studentList = new List<Student>();

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_retrieve_student_by_name";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters and values
            cmd.Parameters.AddWithValue("@name", name);

            // try-catch
            try
            {
                // open connection
                conn.Open();
                // execute command
                var reader = cmd.ExecuteReader();

                // rows ?
                if (reader.HasRows)
                {
                    // process the reader
                    while (reader.Read())
                    {
                        var eq = new Student()
                        {

                            StudentID = reader.GetString(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            PhoneNumber = reader.GetString(3),
                            StudentEmail = reader.GetString(4),
                            Active = reader.GetBoolean(5)
                        };
                        studentList.Add(eq);
                    }
                }
                else // no rows?
                {
                    throw new ApplicationException("Data not found.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database access error.", ex);
            }
            finally
            {
                conn.Close();
            }
            return studentList;
        }
    }
}
