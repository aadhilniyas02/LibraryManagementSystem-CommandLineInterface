using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Project
{ 
public class Library
{
    public SortedDictionary<int, Book> books = new SortedDictionary<int, Book>();
    private SortedDictionary<int, Member> membersNew = new SortedDictionary<int, Member>();
    public List<Book> allbooks = new List<Book>();
    public List<Member> allMembers = new List<Member>();
    
            // Add New Book
            public void AddBook(Book b)
            {
                books.Add(b.BookId, b);
            }

            // Remove  Book
            public void RemoveBook(Book b)
            {
                books.Remove(b.BookId);
            }

            // Add New Member
                public void AddMember(Member m)
            {
                membersNew.Add(m.MemberId, m);
            }

            // Remove Member
            public void RemoveMember(Member m)
            {
                membersNew.Remove(m.MemberId);
            }

            // Non Registered Book Find
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

    }

}
