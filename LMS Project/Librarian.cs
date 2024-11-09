using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Project
{
    public class Librarian : Person
    {

        private SortedDictionary<int, Book> books = new SortedDictionary<int, Book>();
        private SortedDictionary<int, Member> membersNew = new SortedDictionary<int, Member>();
        private Member member;

        // Attributes
        public int LibrarianId { get; set; }

        public string LibrarianName { get; set; }


        // Constructor
        public Librarian(int librarianId, string librarianName, string username, string password) : base(username, password)
        {
            LibrarianId = librarianId;
            LibrarianName = librarianName;
        }


        // Methods 
        public void AddBook(Book b)
        {
            books.Add(b.BookId, b);
        }

        public void RemoveBook(int bookId)
        {
            books.Remove(bookId);
        }


        public void AddMember(Member m)
        {
            membersNew.Add(m.MemberId, m);
        }

        public void RemoveMember(int memberId)
        {
            membersNew.Remove(memberId);
        }


        public List<Member> ViewAllMembers()
        {
            return member.GetAllMembers();
        }

        public List<Loans> ViewCurrentLoans(List<Loans> currentLoans)
        {
            return currentLoans;
        }

    }
}

        
       

