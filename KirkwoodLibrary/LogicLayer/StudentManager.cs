using System;
using System.Collections.Generic;
using System.Text;
using DataTransferObjects;
using DataAccessLayer;
using System.Security.Cryptography;


namespace LogicLayer
{
    public class StudentManager
    {
        public Student AuthenticateUser(string username, string password)
        {
            Student student = null; // user token to build

            // we need to hash the password first
            var passwordHash = HashSha256(password);

            try
            {
                // we need to pass the username and passwordhash
                // to the data access method - if we get back a 1, the user is
                // validated, anything else is unacceptable
                var validationResult = StudentAccessor.VerifyUsernameAndPassword(username, passwordHash);

                if (validationResult == 1) // user is validated
                {
                    // need to get the employee object and roles
                    // to build the user object

                    // first, get the employee
                    student = StudentAccessor.RetrieveStudentByUsername(username);

                    // next, get the employee's roles
                    //var roles = StudentAccessor.RetrieveRolesByEmployeeID(employee.EmployeeID);



                    //  bool passwordMustBeChanged = false;
                    // here's some code to prevent the user from using the app without
                    // changing his or her password first
                    /* if(password=="newuser")
                     {
                         passwordMustBeChanged = true;
                         roles.Clear(); // clear the user's roles so the app can't be used
                         roles.Add(new Role() { RoleID = "New User" });
                     }

                     // and create the user token
                     user = new User(employee, roles, passwordMustBeChanged);
                     */
                }
                else // user was not validated
                {
                    // we can throw an exception here.
                    throw new ApplicationException("Login failed. Bad username (email address) or password");
                }
            }
            catch (ApplicationException) // rethrow the applicaton exception
            {
                throw;
            }
            catch (Exception ex) // wrap and throw other types of exception
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return student;
        }

        // function to apply a SHA256 hash algorithm
        // to a password to store or compare with the
        // user's passwordHash in the database
        private string HashSha256(string source)
        {
            var result = "";

            // create a byte array
            byte[] data;

            // create a .NET Hash provider object
            using (SHA256 sha256hash = SHA256.Create())
            {
                // hash the input
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            // now to build the result string
            var s = new StringBuilder();
            // loop through the byte array creating letters
            // to add to the StringBuilder
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            // get a string from the stringbuilder
            result = s.ToString();

            return result;
        }

        public List<Student> RetrieveStudentList(bool active = true)
        {
            List<Student> studentList = null;

            try
            {
                studentList = StudentAccessor.RetrieveStudentByActive(active);
            }
            catch (Exception)
            {
                throw;
            }

            return studentList;
        }

        public bool DeactivateStudent(Student student)
        {
            bool result = false;

            try
            {
                result = (1 == StudentAccessor.DeactivateStudent(student.StudentID));
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public List<Student> RetrieveStudentListByID(string studentID)
        {
            List<Student> studentDetail = null;

            try
            {

                studentDetail = StudentAccessor.RetrieveStudentListByID(studentID);

            }
            catch (Exception)
            {

                throw;
            }

            return studentDetail;
        }

        public List<Student> RetrieveStudentListByName(string name)
        {
            List<Student> studentDetail = null;

            try
            {

                studentDetail = StudentAccessor.RetrieveStudentListByName(name);

            }
            catch (Exception)
            {

                throw;
            }

            return studentDetail;
        }

    }
}
