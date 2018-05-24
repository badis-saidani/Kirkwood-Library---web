using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects
{
    public class Book
    {
        public int BookID { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Edition { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int EditionYear { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CategoryID { get; set; }
        [Required]
        public int AuthorID { get; set; }
        [Required]
        public string LibraryID { get; set; }
        [Required]
        public string StatusID { get; set; }
        public string StudentEmail { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfCheckout { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateToReturn { get; set; }
        public Boolean Active { get; set; }
    }
}
