using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Project
{
    public class Loans
    {
        List<Loans> loans = new List<Loans>();


        // Attributes 
        public int id { get; }

        public Member Member { get; set; }

        public Book Book { get; set; }

        public DateTime LoanDate { get;  }

        public DateTime DueDate { get; }

        public string ReturnDate
        {
            get { return _returnDate; }
            private set { _returnDate = value; }
        }

        private string _returnDate;


        public Loans(Member member, Book book, DateTime loanDate)
        {
            Member = member;
            book.Status = Book.BookStatus.OnLoan;
            Book = book;
            LoanDate = loanDate;
            DueDate = loanDate.AddDays(14);
            ReturnDate = "------";
        }

        public void EndLoan()
        {
            this.ReturnDate = DateTime.Now.ToString("dd/MM/yyyy");         
        }

    }
}
