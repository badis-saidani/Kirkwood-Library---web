using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;
using DataAccessLayer;
using System.Security.Cryptography;

namespace LogicLayer
{
    public class AdminnManager
    {
        public Adminn AuthenticateUser(string username, string password)
        {
            Adminn adminn = null; // user token to build

            // we need to hash the password first
            var passwordHash = HashSha256(password);

            try
            {
                // we need to pass the username and passwordhash
                // to the data access method - if we get back a 1, the user is
                // validated, anything else is unacceptable
                var validationResult = AdminnAccessor.VerifyUsernameAndPassword(username, passwordHash);

                if (validationResult == 1) // user is validated
                {
                    // need to get the employee object and roles
                    // to build the user object

                    // first, get the employee
                     adminn = AdminnAccessor.RetrieveAdminnByUsername(username);

                    // next, get the employee's roles
                    //var roles = AdminnAccessor.RetrieveRolesByEmployeeID(employee.EmployeeID);



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

            return adminn;
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

      /*  public User UpdatePassword(User user, string oldPassword, string newPassword)
        {
            User newUser = null;
            int rowsAffected = 0;

            string oldPasswordHash = HashSha256(oldPassword);
            string newPasswordHash = HashSha256(newPassword);

            // try to invoke the access method
            try
            {
                rowsAffected = UserAccessor.UpdatePasswordHash(user.Employee.EmployeeID,
                    oldPasswordHash, newPasswordHash);
                if (rowsAffected == 1)
                {
                    if (user.Roles[0].RoleID == "New User")
                    {
                        // get the roles and create a new user
                        var roles = UserAccessor.RetrieveRolesByEmployeeID(user.Employee.EmployeeID);
                        newUser = new User(user.Employee, roles);
                    }
                    else
                    {
                        newUser = user;
                    }
                }
                else
                {
                    throw new ApplicationException("Update returned 0 rows affected.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Password change failed.", ex);
            }

            return newUser;
        }*/
    
    }
}
