using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Project
{
    public class Book
    {
        private SortedDictionary<int, Book> books = new SortedDictionary<int, Book>();

        public enum BookStatus
        {
            Available,
            OnLoan
        }

        // Attributes

        public int BookId { get; set; }

        public string BookName { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public BookStatus Status { get; set; }

        public static string AVAILABLE = "Available";
        public static string ON_LOAN = "On Loan";


        // Constructors
        public Book(int bookId, string bookName, string author, string isbn)
        {
            BookId = bookId;
            BookName = bookName;
            Author = author;
            ISBN = isbn;
            Status = BookStatus.Available;
        }

        //get book details
       public List<Book> GetAllBooks()
        {
            List<Book> list = new List<Book>(books.Count);

            foreach (Book b in books.Values)
            {
                list.Add(b);
            }
            return list;
        }
        public Book FindBook(int bookId)
        {
            try
            {
                return books[bookId];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception($"Book {bookId} does not exist");
            }
        }

        // set book status
        public void SetAvailable()
        {
            Status = BookStatus.Available;
        }

        public void SetOnLoan()
        {
            Status = BookStatus.OnLoan;
        }

    }
}
