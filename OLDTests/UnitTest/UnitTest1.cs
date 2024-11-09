using Xunit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Diagnostics.Metrics;

namespace LMS_Project
{
    public class UnitTest1
    {
        static Stack<Action> menuStack = new Stack<Action>();
        static List<Book> allbooks = new List<Book>();
        static List<Member> allMembers = new List<Member>();
        static Librarian currentLibrarian;
        static Member currentMember;
        static Person currentPerson;
        static List<Book> BorrowedBooks = new List<Book>();

        [Fact]
        public void Librarian_Login_ValidCredentials()
        {
            string username = "admin";
            string password = "adminpass";

            bool isValidLogin = Program.ValidateLibrarianLogin(username, password);
            Assert.True(isValidLogin);
        }
    }
}