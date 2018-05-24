using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class Status
    {
        public string StatusID { get; set; }

        public override string ToString()
        {
            return StatusID;
        }

    }
}
