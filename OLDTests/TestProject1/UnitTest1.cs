
using Xunit;

namespace LMS_Project
{
    public class unitTest
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
        public void Member_Login_ValidCredentials()
        {
            string username = "testuser";
            string password = "testpass";

            Member member = new Member(1, "Test User", username, password);

            bool isValidLogin = Program.ValidateMemberLogin(username, password);
            Assert.True(isValidLogin);
        }

        [Fact]
        public void AddBook_Test()
        {
            Library library = new Library();
            Book book = new Book(1, "Test Book", "Test Author", "1234567890");

            library.AddBook(book);
            Assert.Contains(book, library.allbooks);
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
        public void AddMember_Test()
        {
            Library library = new Library();
            Member member = new Member(1, "Test User", "testuser", "testpass");

            library.AddMember(member);
            Assert.Contains(member, library.allMembers);
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

            member.BorrowBook(library.allbooks, book.BookId);
            Assert.Contains(book, member.BorrowedBooks);
        }

        [Fact]
        public void ReturnBook_Test()
        {
            Library library = new Library();
            Member member = new Member(1, "Test Member", "testuser", "testpass");
            Book book = new Book(1, "Test Book", "Test Author", "1234567890");
            library.AddMember(member);
            library.AddBook(book);
            member.BorrowBook(library.allbooks, book.BookId);

            member.ReturnBook(book.BookId);
            Assert.DoesNotContain(book, member.BorrowedBooks);
        }

        [Fact]
        public void ViewAllBooks_Test()
        {
            Library library = new Library();
            Book book1 = new Book(1, "Book 1", "Author 1", "123456");
            Book book2 = new Book(2, "Book 2", "Author 2", "789012");
            library.AddBook(book1);
            library.AddBook(book2);

            var allBooks = library.allbooks;

            Assert.Contains(book1, allBooks);
            Assert.Contains(book2, allBooks);
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