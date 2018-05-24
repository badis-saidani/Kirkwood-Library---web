using System;

namespace DataTransferObjects
{
    public class Adminn
    {
        public string AdminnID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string AdminnEmail { get; set; }
        public string PasswordHash{ get; set; }
        public bool Active { get; set; }
    }
}
