using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Project
{
   public class Person
    {

        // Attributes
        public string Username { get; set; }
        public string Password { get; set; }


        // Constructor
        public Person( string username, string password )
        {
            Username = username;
            Password = password;
        }

        // Methods
        public List<Book> ViewAllBooks()
        {
            return null;
        }


    }
}
