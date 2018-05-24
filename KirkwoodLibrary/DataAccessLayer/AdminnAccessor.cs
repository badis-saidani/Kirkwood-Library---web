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
     public static class AdminnAccessor
    {
        public static int VerifyUsernameAndPassword(string username, string passwordHash)
        {
            var result = 0; // return the number of rows found (should be 1)

            // need to start with a connection
            var conn = DBConnection.GetDBConnection();

            // next, we need command text - a procedure name or query string
            var cmdText = @"sp_authenticate_adminn";

            // next, we use the connection and command text to create a command
            var cmd = new SqlCommand(cmdText, conn);

            // for a stored procedure, we need to set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // for a stored procedure, add any needed parameters
            cmd.Parameters.Add("@AdminnEmail", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // set the parameter values
            cmd.Parameters["@AdminnEmail"].Value = username;
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

        public static Adminn RetrieveAdminnByUsername(string username)
        {
            Adminn adminn = null;

            // connection first
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_retrieve_adminn_by_email";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@AdminnEmail", SqlDbType.NVarChar, 100);

            // parameeter values
            cmd.Parameters["@AdminnEmail"].Value = username;

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

                    // create a new employee object
                    adminn = new Adminn()
                    {
                        // SELECT 	[EmployeeID], [FirstName], [LastName], [PhoneNumber], [Email], [Active]
                        AdminnID = reader.GetString(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        AdminnEmail = reader.GetString(4),
                        Active = reader.GetBoolean(5)
                    };
                    if (adminn.Active != true)
                    {
                        throw new ApplicationException("Not an active employee.");
                    }
                }
                else
                {
                    throw new ApplicationException("Employee record not found!");
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


            return adminn;
        }

       /* public static Adminn RetrieveAdminnByUsername(string username)
        {
            Adminn adminn = null;

            // connection first
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_retrieve_adminn_by_email";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            // parameeter values
            cmd.Parameters["@Email"].Value = username;

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

                    // create a new employee object
                    employee = new Employee()
                    {
                        // SELECT 	[EmployeeID], [FirstName], [LastName], [PhoneNumber], [Email], [Active]
                        EmployeeID = reader.GetString(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4),
                        Active = reader.GetBoolean(5)
                    };
                    if (employee.Active != true)
                    {
                        throw new ApplicationException("Not an active employee.");
                    }
                }
                else
                {
                    throw new ApplicationException("Employee record not found!");
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


            return employee;
        }*/

      /*  public static List<Role> RetrieveRolesByEmployeeID(string employeeID)
        {
            List<Role> roles = new List<Role>();

            // connection?
            var conn = DBConnection.GetDBConnection();

            // cmdText ?
            var cmdText = @"sp_retrieve_employee_roles";

            // command?
            var cmd = new SqlCommand(cmdText, conn);

            // command type?
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters?
            cmd.Parameters.Add("@EmployeeID", SqlDbType.NVarChar, 20);

            // paramter values?
            cmd.Parameters["@EmployeeID"].Value = employeeID;

            // all set? need a try-catch
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                var reader = cmd.ExecuteReader();

                // check for results
                if (reader.HasRows)
                {
                    // multiple rows are possible, so use a while loop
                    while (reader.Read())
                    {
                        // create role object
                        var role = new Role()
                        {
                            RoleID = reader.GetString(0)
                        };
                        // add the tole to the list
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
        }*/
        
    }
}
