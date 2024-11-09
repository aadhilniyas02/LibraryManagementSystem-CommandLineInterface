using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using static LMS_Project.Book;

namespace LMS_Project
{
    public class Member : Person
    {

        private Dictionary<int, Member> members = new Dictionary<int, Member>();
        public List<Book> BorrowedBooks { get; set; } = new List<Book>();


        // Attributes

        public int MemberId { get; set; }

        public string MemberName { get; set; }

        public int numberOfLoans { get; private set; }

        public BookStatus Status { get; set; }



        // Constructor
        public Member(int memberId, string memberName, string username, string password) : base(username, password)
        {
            MemberId = memberId;
            MemberName = memberName;
            BorrowedBooks = new List<Book>();
        }


        // Method to borrow a book
        public bool BorrowBook(Book book, List<Loans> loans)
        {
            Loans loan = new Loans(this, book, DateTime.Now);
            BorrowedBooks.Add(book);
            loans.Add(loan);
            return true;             
        }

        private void UpdateBookAvailability(List<Book> allBooks, int bookId, Book.BookStatus newStatus)
        {
            Book bookToUpdate = allBooks.FirstOrDefault(book => book.BookId == bookId);
            if (bookToUpdate != null)
            {
                bookToUpdate.Status = newStatus;
            }
            else
            {
                Console.WriteLine($"Error: Book with ID {bookId} not found in all books list.");
            }
        }

        public Member FindMember(int memberId)
        {
            try
            {
                return members[memberId];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception($"Member {memberId} does not exist");
            }
        }

        // Method the Return Book
        public void ReturnBook(int bookId, List<Loans> allLoans)
        {
            if (BorrowedBooks != null)
            {
                Book bookToReturn = BorrowedBooks.FirstOrDefault(book => book.BookId == bookId);

                if (bookToReturn != null)
                {
                    BorrowedBooks.Remove(bookToReturn);
                    bookToReturn.Status = Book.BookStatus.Available;

                    Loans loanToEnd = allLoans.FirstOrDefault(ln => ln.Member == this && ln.Book == bookToReturn);
                    if (loanToEnd != null)
                    {
                        loanToEnd.EndLoan();                      
                        Console.WriteLine($"Book '{bookToReturn.BookName}' returned successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Error: Something wrong, Returned book but no loans found to update return date");
                    }

                }
                else
                {
                    Console.WriteLine($"Error: You have not borrowed the book with ID {bookId}.");
                }
            }
            else
            {
                Console.WriteLine("Error: No Books borrowed");
            }
        }

            public List<Member> GetAllMembers()
            {
                List<Member> list = new List<Member>(members.Count);

                foreach (Member m in members.Values)
                {
                    list.Add(m);
                }
                return list;
            }

        public List<Loans> ViewCurrentLoans(List<Loans> currentLoans)
        {
            return currentLoans.FindAll(loan => loan.Member == this);
        }

    }
}

