using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects
{
    public class Library
    {
        public string LibraryID { get; set; }
        public override string ToString()
        {
            return LibraryID;
        }
        // public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }

    }
}
