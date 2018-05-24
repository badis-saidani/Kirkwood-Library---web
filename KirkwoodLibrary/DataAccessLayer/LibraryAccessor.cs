using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects;

namespace DataAccessLayer
{
    public class LibraryAccessor
    {
        public static List<Library> RetrieveLibraryList()
        {
            var libraryList = new List<Library>();

            // connection 
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_select_library_list";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command Type

            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            // cmd.Parameters.AddWithValue("@Active", active);


            //try catch 
            try
            {
                // open Connection
                conn.Open();
                // execute command
                var reader = cmd.ExecuteReader();

                // rows ?
                if (reader.HasRows)
                {
                    // process the reader 
                    while (reader.Read())
                    {


                        var eq = new Library()
                        {


                            LibraryID = reader.GetString(0)

                        };
                        libraryList.Add(eq);
                    }

                }
                else
                {
                    throw new ApplicationException("Data not found.");
                }

            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error.", ex);
            }
            finally
            {
                conn.Close();
            }

            return libraryList;
        }
    }
}
