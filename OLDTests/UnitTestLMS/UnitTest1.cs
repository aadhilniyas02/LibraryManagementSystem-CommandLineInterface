using Xunit;
using LMS_Project;


namespace UnitTestLMS
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
    }
}