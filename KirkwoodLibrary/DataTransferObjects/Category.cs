using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects
{
    public class Category
    {
        public string CategoryID { get; set; }
        public override string ToString()
        {
            return CategoryID;
        }
    }
}
