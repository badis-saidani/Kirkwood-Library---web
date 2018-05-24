using System;
using System.Collections.Generic;
using System.Text;
using DataTransferObjects;
using DataAccessLayer;

namespace LogicLayer
{
    public class BookManager
    {
        public List<Book> RetrieveBookList(bool active = true)
        {
            List<Book> bookList = null;

            try
            {
                bookList = BookAccessor.RetrieveBookByActive(active);
            }
            catch (Exception)
            {
                throw;
            }

            return bookList;
        }

        public Book RetrieveBookByID(Book bookItem)
        {
            Book bookDetail = null;

            try
            {
              
                bookDetail = BookAccessor.RetrieveBookByID(bookItem.BookID);
                
            }
            catch (Exception)
            {

                throw;
            }

            return bookDetail;
        }

        public Book RetrieveBookByID(int id)
        {
            Book bookDetail = null;

            try
            {

                bookDetail = BookAccessor.RetrieveBookByID(id);

            }
            catch (Exception)
            {

                throw;
            }

            return bookDetail;
        }

        public List<Book> RetrieveBookListByID(int bookID)
        {
            List<Book> bookDetail = null;

            try
            {

                bookDetail = BookAccessor.RetrieveBookListByID(bookID);

            }
            catch (Exception)
            {

                throw;
            }

            return bookDetail;
        }

        public List<Book> RetrieveBookListByWord(string word)
        {
            List<Book> bookDetail = null;

            try
            {

                bookDetail = BookAccessor.RetrieveBookByWords(word);

            }
            catch (Exception)
            {

                throw;
            }

            return bookDetail;
        }

        public List<Status> RetrieveStatusList()
        {
            try
            {
                return BookAccessor.RetrieveStatusList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Category> RetrieveCategoryList()
        {
            try
            {
                return BookAccessor.RetrieveCategoryList();
            }
            catch (Exception)
            {

                throw;
            }
        }

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

        public List<Author> RetrieveAuthorList()
        {
            try
            {
                return BookAccessor.RetrieveAuthorList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SaveNewBook(Book book)
        {
            var result = false;

            if (book.ISBN == ""  || book.Title == "" || book.Edition == "" 
                || book.EditionYear == 0 || book.CategoryID == null ||
                book.AuthorID < 10000)
            {
                throw new ApplicationException("You must fill out all the fields.");
            }
            try
            {
                result = (0 != BookAccessor.InsertBook(book));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool EditBook(Book book, Book oldBook, string updateType)
        {
            var result = false;

            if (book.ISBN == "" || book.Title == "" || book.Edition == ""
                || book.EditionYear < 800 ||
                book.AuthorID < 100000)
            {
                throw new ApplicationException("You must fill out all the fields.");
            }
            try
            {
                result = (0 != BookAccessor.UpdateBook(book, oldBook, updateType));
            }
            catch (Exception)
            {
                throw;
            }
            return result;

            
        }

        public bool DeactivateBook(Book book)
        {
            bool result = false;

            try
            {
                result = (1 == BookAccessor.DeactivateBook(book.BookID));
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public bool DeleteBook(Book book)
        {
            bool result = false;

            try
            {
                result = (1 == BookAccessor.DeleteBook(book.BookID));
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public bool HoldBook(Book book, string studentID)
        {
            bool result = false;

            try
            {
                result = (1 == BookAccessor.HoldBook(book.BookID, studentID));
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }




    }
}
