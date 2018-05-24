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
    public static class BookAccessor
    {
        public static List<Book> RetrieveBookByActive(bool active = true)
        {
            var bookList = new List<Book>();

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_select_book_by_active";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters and values
            cmd.Parameters.AddWithValue("@Active", active);

            // try-catch
            try
            {
                // open connection
                conn.Open();
                // execute command
                var reader = cmd.ExecuteReader();

                // rows ?
                if (reader.HasRows)
                {
                    // process the reader
                    while (reader.Read())
                    {
                        var eq = new Book()
                        {



                            /*  [BookID],[Name],[Description],[StatusID],
                                [BookModelID], [InspectionListID],
                                [BookID],[Active] */
                            BookID = reader.GetInt32(0),
                            ISBN = reader.GetString(1),
                            Edition = reader.GetString(2),
                            Title = reader.GetString(3),
                            EditionYear = reader.GetInt32(4),
                            Description = reader.GetString(5),
                            CategoryID = reader.GetString(6),
                            AuthorID = reader.GetInt32(7),
                            LibraryID = reader.GetString(8),
                            StatusID = reader.GetString(9),
                            StudentEmail = reader.IsDBNull(10) ? null : reader.GetString(10), // null
                            DateOfCheckout = reader.IsDBNull(11) ? null : (DateTime?)reader.GetDateTime(11), // null
                            DateToReturn = reader.IsDBNull(12) ? null : (DateTime?)reader.GetDateTime(12), // null
                            Active = reader.GetBoolean(13)
                        };
                        bookList.Add(eq);
                    }
                }
                else // no rows?
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
            return bookList;
        }

        public static List<Book> RetrieveBookByWords(string word)
        {
            var bookList = new List<Book>();

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_select_book_by_words";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters and values
            cmd.Parameters.AddWithValue("@Words", word);

            // try-catch
            try
            {
                // open connection
                conn.Open();
                // execute command
                var reader = cmd.ExecuteReader();

                // rows ?
                if (reader.HasRows)
                {
                    // process the reader
                    while (reader.Read())
                    {
                        var eq = new Book()
                        {



                            /*  [BookID],[Name],[Description],[StatusID],
                                [BookModelID], [InspectionListID],
                                [BookID],[Active] */
                            BookID = reader.GetInt32(0),
                            ISBN = reader.GetString(1),
                            Edition = reader.GetString(2),
                            Title = reader.GetString(3),
                            EditionYear = reader.GetInt32(4),
                            Description = reader.GetString(5),
                            CategoryID = reader.GetString(6),
                            AuthorID = reader.GetInt32(7),
                            LibraryID = reader.GetString(8),
                            StatusID = reader.GetString(9),
                            StudentEmail = reader.IsDBNull(10) ? null : reader.GetString(10), // null
                            DateOfCheckout = reader.IsDBNull(11) ? null : (DateTime?)reader.GetDateTime(11), // null
                            DateToReturn = reader.IsDBNull(12) ? null : (DateTime?)reader.GetDateTime(12), // null
                            Active = reader.GetBoolean(13)
                        };
                        bookList.Add(eq);
                    }
                }
                else // no rows?
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
            return bookList;
        }

        public static Book RetrieveBookByID(int bookID)
        {
            Book book = null;

            // connection 
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_select_book_by_id";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command Type

            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@BookID", bookID);


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

                    reader.Read();

                    book = new Book()
                    {
                        BookID = reader.GetInt32(0),
                        ISBN = reader.GetString(1),
                        Edition = reader.GetString(2),
                        Title = reader.GetString(3),
                        EditionYear = reader.GetInt32(4),
                        Description = reader.GetString(5),
                        CategoryID = reader.GetString(6),
                        AuthorID = reader.GetInt32(7),
                        LibraryID = reader.GetString(8),
                        StatusID = reader.GetString(9),
                        StudentEmail = reader.IsDBNull(10) ? null : reader.GetString(10), // null
                        DateOfCheckout = reader.IsDBNull(11) ? null : (DateTime?)reader.GetDateTime(11), // null
                        DateToReturn = reader.IsDBNull(12) ? null : (DateTime?)reader.GetDateTime(12), // null
                        Active = reader.GetBoolean(13)
                    };



                }
                else
                {
                    throw new ApplicationException("Record not found.");
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

            return book;
        }

        public static List<Status> RetrieveStatusList()
        {
            var statusList = new List<Status>();

            // connection 
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_select_status_list";

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


                        var eq = new Status()
                        {


                            StatusID = reader.GetString(0)

                        };
                        statusList.Add(eq);
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

            return statusList;
        }

        public static List<Category> RetrieveCategoryList()
        {
            var categoryList = new List<Category>();

            // connection 
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_select_category_list";

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


                        var eq = new Category()
                        {


                            CategoryID = reader.GetString(0)

                        };
                        categoryList.Add(eq);
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

            return categoryList;
        }

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

        public static List<Author> RetrieveAuthorList()
        {
            var authorList = new List<Author>();

            // connection 
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_select_author_list";

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


                        var eq = new Author()
                        {
                            AuthorID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Description = reader.GetString(3)

                        };
                        authorList.Add(eq);
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

            return authorList;
        }

        public static int InsertBook(Book book)
        {
            int newId = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_insert_book";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
            cmd.Parameters.AddWithValue("@Edition", book.Edition);
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@EditionYear", book.EditionYear);
            cmd.Parameters.AddWithValue("@Description", book.Description);
            cmd.Parameters.AddWithValue("@CategoryID", book.CategoryID);
            cmd.Parameters.AddWithValue("@AuthorID", book.AuthorID);
            cmd.Parameters.AddWithValue("@LibraryID", book.LibraryID);
            cmd.Parameters.AddWithValue("@StatusID", book.StatusID);
            //cmd.Parameters.AddWithValue("@StudentID", book.StudentID);
            //cmd.Parameters.AddWithValue("@DateOfCheckout", book.DateOfCheckout);
            //cmd.Parameters.AddWithValue("@DateToReturn", book.DateToReturn);

            try
            {
                conn.Open();
                decimal id = (decimal)cmd.ExecuteScalar();
                newId = (int)id;
            }
            catch (Exception)
            {
                throw;
            }

            return newId;
        }


        public static int UpdateBook(Book book, Book oldBook, string updateType)
        {
            int rows = 0;


            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_update_book";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BookID", book.BookID);
            cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
            cmd.Parameters.AddWithValue("@Edition", book.Edition);
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@EditionYear", book.EditionYear);
            cmd.Parameters.AddWithValue("@Description", book.Description);
            cmd.Parameters.AddWithValue("@CategoryID", book.CategoryID);
            cmd.Parameters.AddWithValue("@AuthorID", book.AuthorID);
            cmd.Parameters.AddWithValue("@LibraryID", book.LibraryID);
            cmd.Parameters.AddWithValue("@StatusID", book.StatusID);    /****************/
            cmd.Parameters.AddWithValue("@OldISBN", oldBook.ISBN);
            cmd.Parameters.AddWithValue("@OldEdition", oldBook.Edition);
            cmd.Parameters.AddWithValue("@OldTitle", oldBook.Title);
            cmd.Parameters.AddWithValue("@OldEditionYear", oldBook.EditionYear);
            cmd.Parameters.AddWithValue("@OldDescription", oldBook.Description);
            cmd.Parameters.AddWithValue("@OldCategoryID", oldBook.CategoryID);
            cmd.Parameters.AddWithValue("@OldAuthorID", oldBook.AuthorID);
            cmd.Parameters.AddWithValue("@OldLibraryID", oldBook.LibraryID);
            cmd.Parameters.AddWithValue("@OldStatusID", oldBook.StatusID);

            if (updateType == "In Held")
            {
                cmd.Parameters.AddWithValue("@StudentEmail", book.StudentEmail);
                cmd.Parameters.AddWithValue("@DateOfCheckout", DBNull.Value);
                cmd.Parameters.AddWithValue("@DateToReturn",  DBNull.Value);
                // cmd.Parameters.AddWithValue("@OldStudentID", oldBook.StudentID);
            }
            else if (updateType == "Out")
            {
                cmd.Parameters.AddWithValue("@StudentEmail", book.StudentEmail);
                cmd.Parameters.AddWithValue("@DateOfCheckout", book.DateOfCheckout);
                cmd.Parameters.AddWithValue("@DateToReturn", book.DateToReturn);
               // cmd.Parameters.AddWithValue("@OldDateOfCheckout", oldBook.DateOfCheckout);
              //  cmd.Parameters.AddWithValue("@OldDateToReturn", oldBook.DateToReturn);
            }
            else if (updateType == "Available")
            {
                cmd.Parameters.AddWithValue("@StudentEmail", DBNull.Value);
                cmd.Parameters.AddWithValue("@DateOfCheckout",  DBNull.Value);
                cmd.Parameters.AddWithValue("@DateToReturn", DBNull.Value);
                // cmd.Parameters.AddWithValue("@OldDateOfCheckout", oldBook.DateOfCheckout);
                //  cmd.Parameters.AddWithValue("@OldDateToReturn", oldBook.DateToReturn);
            }




            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
               // MessageBox.Show(ex.Message);
            }

            return rows;
        }

        public static int DeactivateBook(int bookID)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_book";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookID", bookID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return rows;
        }

        public static int DeleteBook(int bookID)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_book";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookID", bookID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return rows;
        }

        public static int HoldBook(int bookID, string studentEmail)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_hold_book_by_student_email";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookID", bookID);
            cmd.Parameters.AddWithValue("@StudentEmail", studentEmail);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return rows;
        }

        public static List<Book> RetrieveBookListByID(int bookID)
        {
            var bookList = new List<Book>();

            // connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_select_book_by_id";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters and values
            cmd.Parameters.AddWithValue("@BookID", bookID);

            // try-catch
            try
            {
                // open connection
                conn.Open();
                // execute command
                var reader = cmd.ExecuteReader();

                // rows ?
                if (reader.HasRows)
                {
                    // process the reader
                    while (reader.Read())
                    {
                        var eq = new Book()
                        {



                            /*  [BookID],[Name],[Description],[StatusID],
                                [BookModelID], [InspectionListID],
                                [BookID],[Active] */
                            BookID = reader.GetInt32(0),
                            ISBN = reader.GetString(1),
                            Edition = reader.GetString(2),
                            Title = reader.GetString(3),
                            EditionYear = reader.GetInt32(4),
                            Description = reader.GetString(5),
                            CategoryID = reader.GetString(6),
                            AuthorID = reader.GetInt32(7),
                            LibraryID = reader.GetString(8),
                            StatusID = reader.GetString(9),
                            StudentEmail = reader.IsDBNull(10) ? null : reader.GetString(10), // null
                            DateOfCheckout = reader.IsDBNull(11) ? null : (DateTime?)reader.GetDateTime(11), // null
                            DateToReturn = reader.IsDBNull(12) ? null : (DateTime?)reader.GetDateTime(12), // null
                            Active = reader.GetBoolean(13)
                        };
                        bookList.Add(eq);
                    }
                }
                else // no rows?
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
            return bookList;
        }


    }
}
