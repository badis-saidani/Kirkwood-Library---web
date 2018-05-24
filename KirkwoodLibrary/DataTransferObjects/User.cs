using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class User
    {
        public Student Student { get; private set; }
        public List<Role> Roles { get; private set; }
        public bool PasswordMustBeChanged { get; private set; }

        public User(Student Student, List<Role> roles,
            bool passwordMustBeChanged = false)
        {
            this.Student = Student;
            this.Roles = roles;
            this.PasswordMustBeChanged = passwordMustBeChanged;
        }
    }
}
