using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using Xunit;
using LMS_Project;
using System.Diagnostics.Metrics;


namespace LMS_Project.Test
{
    public class UnitTest1
    {

        [Fact]
        public void Librarian_Login_ValidCredentials()
        {
            string username = "admin";
            string password = "adminpass";

            bool isValidLogin = Program.ValidateLibrarianLogin(username, password);
            Assert.True(isValidLogin);
        }


        [Fact]
        public void AddBook_Test()
        {
            Librarian librarian = new Librarian(1, "admin", "admin", "adminpass");
            Book book = new Book(1, "Test Book", "Test Author", "1234567890");
            List<Book> allBooks = new List<Book> { book };

            librarian.AddBook(book);
            Assert.Contains(book, allBooks);
        }

        [Fact]
        public void RemoveBook_Test()
        {
            Library library = new Library();
            Book book = new Book(1, "Test Book", "Test Author", "1234567890");
            library.AddBook(book);

            library.RemoveBook(book);
            Assert.DoesNotContain(book, library.allbooks);
        }


        [Fact]
        public void RemoveMember_Test()
        {
            Library library = new Library();
            Member member = new Member(1, "Test User", "testuser", "testpass");
            library.AddMember(member);

            library.RemoveMember(member);
            Assert.DoesNotContain(member, library.allMembers);
        }

        [Fact]
        public void BorrowBook_Test()
        {
            Library library = new Library();
            Member member = new Member(1, "Test Member", "testuser", "testpass");
            Book book = new Book(1, "Test Book", "Test Author", "1234567890");
            library.AddMember(member);
            library.AddBook(book);

            Book bookToBorrow = new Book(1, "Book Title", "Author Name", "ISBN Number");

            List<Loans> loansList = new List<Loans>();
            member.BorrowBook(bookToBorrow, loansList);
            Assert.Contains(bookToBorrow, member.BorrowedBooks);
        }


        [Fact]
        public void ReturnBook_Test()
        {
            Library library = new Library();
            Member member = new Member(1, "Test Member", "testuser", "testpass");
            Book book = new Book(1, "Test Book", "Test Author", "1234567890");
            library.AddMember(member);
            library.AddBook(book);

            List<Loans> loansList = new List<Loans>();
            member.BorrowBook(book, loansList);

            member.ReturnBook(book.BookId, loansList);
            Assert.DoesNotContain(book, member.BorrowedBooks);
        }

        [Fact]
        public void ViewAllBooks_Test()
        {
            Library library = new Library();
            Book expectedBook = new Book(1, "Book 1", "Author 1", "123456");
            library.AddBook(expectedBook);

            List<Book> allBooks = library.books.Values.ToList();
            Assert.Contains(expectedBook, allBooks);
        }


        [Fact]
        public void Member_Login_ValidCredentials()
        {
            string username = "testuser";
            string password = "testpass";

            Member member = new Member(1, "Test Book", "Test Author", "1234567890");
            bool isValidLogin = Program.ValidateMemberLogin(username, password);
            Assert.True(isValidLogin);
        }

        [Fact]
        public void AddMember_Test()
        {
            Library library = new Library();
            Member newMember = new Member(1, "Test User", "testuser", "testpass");

            library.AddMember(newMember);
            Assert.Contains(newMember, library.allMembers);
        }

        [Fact]
        public void ViewAllMembers_Test()
        {
            Library library = new Library();
            Member member1 = new Member(1, "Member 1", "user1", "pass1");
            Member member2 = new Member(2, "Member 2", "user2", "pass2");

            library.AddMember(member1);
            library.AddMember(member2);

            var allMembers = library.allMembers;
            Assert.Contains(member1, allMembers);
            Assert.Contains(member2, allMembers);
        }

    }
}