using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataTransferObjects;
using System.Security.Cryptography;

namespace LogicLayer
{
    public class UserManager
    {
        public User AuthenticateUser(string username, string password)
        {
            User user = null;
            // we can test for password complexity here, but won't for now


            // first, hash the password
            var passwordHash = hashSha256(password);

            try
            {
                // we want to get a 1 as a result of calling the access method
                if (1 == StudentAccessor.VerifyUsernameAndPassword(username, passwordHash))
                {
                    // get the Student object
                    var Student = StudentAccessor.RetrieveStudentByUsername(username);

                    // get the list of roles
                    var roles = StudentAccessor.RetrieveStudentRoles(Student.StudentID);
                    // check to see if the password needs changing
                    bool passwordNeedsChanging = false;
                    //if(password == "newuser") // add additional reasons as needed
                    //{
                    //    passwordNeedsChanging = true;
                    //    roles.Clear();
                    //    roles.Add(new Role() { RoleID = "New User" });
                    //}
                    // we might want to include code to invalidate the user, say
                    // by clearing the roles list if the user's password is expired
                    // such as with user.Roles.Clear();

                    user = new User(Student, roles, passwordNeedsChanging);

                }
                else // got back 0
                {
                    throw new ApplicationException("Bad username or password.");
                }
            }
            catch (Exception ex)  // other exceptions are possible (SqlException)
            {
                // wrap the exception in one with a friendlier message.
                throw new ApplicationException("Login Failure!", ex);
            }


            return user;
        }

        //public User UpdatePassword(User user, string oldPassword, string newPassword)
        //{
        //    User newUser = null;
        //    int rowsAffected = 0;

        //    string oldPasswordHash = hashSha256(oldPassword);
        //    string newPasswordHash = hashSha256(newPassword);

        //    // try to invoke the data access method
        //    try
        //    {
        //        rowsAffected = UserAccessor.UpdatePasswordHash(user.Student.StudentID,
        //                                        oldPasswordHash, newPasswordHash);
        //        if (rowsAffected == 1) // update succeeded
        //        {
        //            if (user.Roles[0].RoleID == "New User")
        //            {
        //                var roles = UserAccessor.RetrieveStudentRoles(user.Student.StudentID);
        //                newUser = new User(user.Student, roles);
        //            }
        //            else
        //            {
        //                newUser = user; // existing user, nothing to change
        //            }
        //        }
        //        else
        //        {
        //            throw new ApplicationException("Update returned 0 rows affected.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException("Password change failed.", ex);
        //    }

        //    return newUser;
        //}

        private string hashSha256(string source)
        {
            string result = null;

            // we need a byte array to process
            // the bytes in the source string
            byte[] data;

            // this using() statement is commonly used to create
            // block-level code when no block statement is needed.
            // that allows us to use computationally expensive
            // classes, and be sure they are disposed immediately.
            using (SHA256 sha256hasher = SHA256.Create())
            {
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            // next, cycle through the bytes, adding them to a stringbuilder
            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                // read each byte as a number, convert it to a string
                // formatted as hex digits, not as decimal digits
                s.Append(data[i].ToString("x2"));
            }

            result = s.ToString(); // create the return string

            return result;
        }
    }
}
