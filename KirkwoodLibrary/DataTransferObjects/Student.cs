using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects
{
    public class Student
    {
        public string StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string StudentEmail { get; set; }
        public string PasswordHash { get; set; }
        public bool Active { get; set; }
    }
}
