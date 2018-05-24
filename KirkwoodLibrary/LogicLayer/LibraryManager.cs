using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataTransferObjects;

namespace LogicLayer
{
    public class LibraryManager
    {
        public List<Library> RetrieveLibraryList()
        {
            try
            {
                return BookAccessor.RetrieveLibraryList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
