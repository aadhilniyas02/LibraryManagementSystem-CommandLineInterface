using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LMS_Project
{
    public class Program
    {
        static Stack<Action> menuStack = new Stack<Action>();
        static List<Book> allbooks = new List<Book>();
        static List<Member> allMembers = new List<Member>();
        static Librarian currentLibrarian;
        static Member currentMember;
        static Person currentPerson;
        static List<Book> BorrowedBooks = new List<Book>();
        static List<Loans> allLoans = new List<Loans>();


        // LMS main 
        public static void Main()
        {
            try
            {
                int choice;
                bool isValidChoice;

                do
                {
                    Console.WriteLine("Choose Login Method :");
                    Console.WriteLine("1. Librarian");
                    Console.WriteLine("2. Member");
                    Console.WriteLine("3. Exit");

                    string input = Console.ReadLine();
                    isValidChoice = int.TryParse(input, out choice);

                    if (!isValidChoice)
                    {
                        Console.WriteLine("Invalid Input. Please Enter a Valid Number (1 or 2).");
                    }
                    else
                    {
                        switch (choice)
                        {
                            case 1:
                                LibrarianLogin();
                                break;
                            case 2:
                                MemberLogin();
                                break;
                            case 3:
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Invalid Choice. Try Again.");
                                isValidChoice = false;
                                break;
                        }
                    }
                } while (!isValidChoice);
            }
            catch
            {
                Console.WriteLine("Program Execute Complete.");
            }
        }

        // Librarian Login Method
        public static void LibrarianLogin()
        {
            const int maxAttemptss = 3;
            int attempts = 0;

            while (attempts < maxAttemptss)
            {
                try
                {
                    Console.WriteLine("Librarian Login : ");
                    Console.WriteLine("Enter User Name : ");
                    string username = Console.ReadLine();
                    Console.WriteLine("Enter Password : ");
                    string password = Console.ReadLine();

                    bool isValidLogin = ValidateLibrarianLogin(username, password);

                    if (isValidLogin)
                    {
                        currentPerson = currentLibrarian = new Librarian(1, "John", username, password);
                        LibrarianOptionss();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Username or Passsword. Please Try Again.");
                        attempts++;
                    }
                }
                catch
                {
                    Console.WriteLine($"Login attempt {attempts} complete.\n");
                }
            }
            Console.WriteLine("Maximum Login Attempts Reached. Exiting Program.");
            Environment.Exit(0);
        }

        // Librarians Username and Password Confirmation
        public static bool ValidateLibrarianLogin(string username, string password)
        {
            return username == "admin" && password == "adminpass";
        }

        // Librarian Options
        public static void LibrarianOptionss()
        {

            try
            {
                while (true)
                {
                    Console.WriteLine("Librarian Options :");
                    Console.WriteLine("1. Add Book");
                    Console.WriteLine("2. Remove Book");
                    Console.WriteLine("3. Add Member");
                    Console.WriteLine("4. Remove Member");
                    Console.WriteLine("5. View All Books ");
                    Console.WriteLine("6. View Available Books ");
                    Console.WriteLine("7. View Book Loans/ Transactions");
                    Console.WriteLine("8. View All Members");
                    Console.WriteLine("9. Menu");
                    Console.WriteLine("10. Logout");
                    Console.WriteLine("11. Exit");

                    int option = Convert.ToInt32(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            AddBook();
                            break;

                        case 2:
                            RemoveBook();
                            break;

                        case 3:
                            AddMember();
                            break;

                        case 4:
                            RemoveMember();
                            break;

                        case 5:
                            ViewAllBooksList();
                            break;

                        case 6:
                            ViewAvailableBooks();
                            break;

                        case 7:
                            ViewCurrentLoansDetails();
                            break;

                        case 8:
                            ViewAllMembers();
                            break;

                        case 9:
                            Menu();
                            break;

                        case 10:
                            Logout();
                            break;

                        case 11:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Invalid Option. Please Try Again.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Librarian options: {ex.Message}");
            }
        }


        // Add Book - librarian
        public static void AddBook()
        {
            try
            {
                Console.WriteLine("Enter Book ID :");
                int bookId;

                while (true)
                {
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out bookId) && input.Length >= 1 && input.Length <= 2)
                    {
                        Book bookToAdd = allbooks.Find(book => book.BookId == bookId);
                        if (bookToAdd != null)
                        {
                            Console.WriteLine($"Book with ID {bookId} is already available, Enter a different ID.");                           
                        }
                        else
                        {
                            break;
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter an integer with 1 or 2 digits only.");
                    }
                }

                Console.WriteLine("Enter Book Name :");
                string bookName;
                while (true)
                {
                    bookName = Console.ReadLine();

                    if (IsAlphabet(bookName))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter alphabets only.");
                    }
                }

                Console.WriteLine("Enter Author Name:");
                string author;
                while (true)
                {
                    author = Console.ReadLine();

                    if (IsAlphabet(author))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter alphabets only.");
                    }
                }

                Console.WriteLine("Enter ISBN :");
                string isbn;
                while (true)
                {
                    isbn = Console.ReadLine();

                    if (isbn.Length == 3 && isbn.All(char.IsDigit))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter exactly 3 integer characters.");
                    }
                }


                Book newBook = new Book(bookId, bookName, author, isbn);
                allbooks.Add(newBook);
                Console.WriteLine($"{bookName} Added Successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        private static bool IsAlphabet(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && input.All(char.IsLetter);
        }

        // Remove a Book - librarian
        public static void RemoveBook()
        {
            Console.WriteLine("Enter Book Id to Remove : ");
            int bookIdToRemove = Convert.ToInt32(Console.ReadLine());

            Book bookToRemove = allbooks.Find(book => book.BookId == bookIdToRemove);
            if (bookToRemove != null)
            {
                allbooks.Remove(bookToRemove);
                Console.WriteLine($"Book with Id {bookIdToRemove} Removed Successfully.");
            }
            else
            {
                Console.WriteLine($"Book with Id {bookIdToRemove} Not Found.");
            }
        }

        // Add Member - librarian
        public static void AddMember()
        {
            try
            {
                Console.WriteLine("Enter Member ID :");
                int memberId;
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out memberId) && memberId >= 0 && memberId <= 99)
                    {

                        Member memberToAdd = allMembers.Find(m => m.MemberId == memberId);
                        if (memberToAdd != null)
                        {
                            Console.WriteLine($"Member with ID {memberId} is already available, Enter a different ID.");
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer between 0 and 99.");
                    }
                }

                Console.WriteLine("Enter Member Name :");
                string memberName;
                while (true)
                {
                    memberName = Console.ReadLine();
                    if (IsAlphabetTwo(memberName))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter alphabets only.");
                    }
                }

                Console.WriteLine("Enter Member User Name :");
                string username;
                while (true)
                {
                    username = Console.ReadLine();
                    if (IsAlphabetTwo(username))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter alphabets only.");
                    }
                }

                Console.WriteLine("Enter Member Password :");
                string password;
                while (true)
                {

                    password = Console.ReadLine();
                    if (IsAlphabetTwo(password))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter alphabets only.");
                    }
                }

                Member newMember = new Member(memberId, memberName, username, password);
                allMembers.Add(newMember);
                Console.WriteLine($"{memberName} Added Successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



        private static bool IsAlphabetTwo(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && input.All(char.IsLetter);
        }



        // Remove a Member - librarian
        public static void RemoveMember()
        {
            Console.WriteLine("Enter Member Id to Remove : ");
            int memberIdToRemove = Convert.ToInt32(Console.ReadLine());

            Member memberToRemove = allMembers.Find(member => member.MemberId == memberIdToRemove);
            if (memberToRemove != null)
            {
                allMembers.Remove(memberToRemove);
                Console.WriteLine($"Member with ID {memberIdToRemove} Removed Successfully. ");
            }
            else
            {
                Console.WriteLine($"Member with ID {memberIdToRemove} Not Found. ");
            }
        }

        // View All Books List - librarian and Member Option
        public static void ViewAllBooksList()
        {
            Console.WriteLine("\nAll Books");
            Console.WriteLine("\t{0, -4} {1, -20} {2, -20} {3, -10}   {4}", "ID", "Name", "Author", "ISBN", "Status");

            foreach (Book b in allbooks)
            {
                DisplayBookList(b);
            }
        }


        public static void DisplayBookList(Book book)
        {
            Console.WriteLine(
                "\t{0, -4} {1, -20} {2, -20} {3, -10}    {4}",
                book.BookId,
                book.BookName,
                book.Author,
                book.ISBN,
                book.Status);
        }


        // View Available Books - librarian and Member Option
        public static void ViewAvailableBooks()
        {
            Console.WriteLine("\nAvailable Books");
            Console.WriteLine("\t{0, -4} {1, -20} {2, -20} {3, -10}", "ID", "Name", "Author", "ISBN");

            foreach (Book book in allbooks)
            {
                if (book.Status == Book.BookStatus.Available)
                {
                    DisplayAvailableBooks(book);
                }
            }
        }


        public static void DisplayAvailableBooks(Book book)
        {
            Console.WriteLine(
                "\t{0, -4} {1, -20} {2, -20} {3, -10}",
                book.BookId,
                book.BookName,
                book.Author,
                book.ISBN);
        }





        // View All Members - librarian Option
        public static void ViewAllMembers()
        {
            Console.WriteLine("\nAll Members");
            Console.WriteLine("\t{0, -4} {1, -20}", "ID", "Member Name");

            foreach (Member member in allMembers)
            {
                DisplayMemberDetails(member);
            }
        }

        // Display  All Members - librarian
        public static void DisplayMemberDetails(Member member)
        {
            Console.WriteLine("\t{0, -4} {1, -20}", member.MemberId, member.MemberName);
        }


        // Member Login Method
        public static void MemberLogin()
        {
            const int maxAttempts = 3;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                try
                {
                    Console.WriteLine("Member Login : ");
                    Console.WriteLine("Enter Username : ");
                    string username = Console.ReadLine();
                    Console.WriteLine("Enter Password : ");
                    string password = Console.ReadLine();

                    // Validate Member Login
                    bool isValidLogin = ValidateMemberLogin(username, password);

                    if (isValidLogin)
                    {

                        currentPerson = currentMember = allMembers.FirstOrDefault(member => member.Username == username);
                        MemberOptions();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Username or Password. Please Try Again.");
                        attempts++;
                    }
                }
                catch
                {
                    Console.WriteLine($"Login attempt {attempts} complete.\n");
                }
            }
            Console.WriteLine("Maximum Login Attempts Reached. Exiting Program.");
            Environment.Exit(0);
        }



        // Validate Member Login
        public static bool ValidateMemberLogin(string username, string password)
        {

            return allMembers.Any(member => member.Username == username && member.Password == password);
        }



        // Member Options
        public static void MemberOptions()
        {

            try
            {
                while (true)
                {
                    Console.WriteLine("Member Options :");
                    Console.WriteLine("1. Borrow Book");
                    Console.WriteLine("2. Return Book");
                    Console.WriteLine("3. View All Books ");
                    Console.WriteLine("4. View Available Books ");
                    Console.WriteLine("5. View Book Loans/ Transactions");
                    Console.WriteLine("6. View Borrowed Books");
                    Console.WriteLine("7. Menu");
                    Console.WriteLine("8. Logout");
                    Console.WriteLine("9. Exit");

                    int option = Convert.ToInt32(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            BorrowBook();
                            break;

                        case 2:
                            ReturnBook(currentMember, allLoans, allbooks);
                            break;

                        case 3:
                            ViewAllBooksList();
                            break;

                        case 4:
                            ViewAvailableBooks();
                            break;

                        case 5:
                            ViewCurrentLoansDetails();
                            break;

                        case 6:
                            ViewBorrowedBooks();
                            break;

                        case 7:
                            Menu();
                            break;

                        case 8:
                            Logout();
                            break;

                        case 9:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Invalid Option. Please Try Again.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Librarian options: {ex.Message}");
            }
        }

        public static int ReadInteger(string prompt)
        {
            try
            {
                Console.Write(prompt + ": > ");
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                return -1;
            }
        }


        // Borrow Book - Member Option
        public static void BorrowBook()
        {
            try
            {
                if (!(currentPerson is Member))
                {
                    Console.WriteLine("Error: You are not logged in as a Member.");
                    return;
                }

                currentMember = (Member)currentPerson;

                Console.WriteLine("Enter Book ID to Borrow: ");
                int bookIdToBorrow = Convert.ToInt32(Console.ReadLine());

                Book bookToBorrow = allbooks.FirstOrDefault(book => book.BookId == bookIdToBorrow);
                if (bookToBorrow == null)
                {
                    Console.WriteLine($"Error: Book with ID {bookIdToBorrow} does not exist.");
                    return;
                }

                if (bookToBorrow.Status != Book.BookStatus.Available)
                {
                    Console.WriteLine($"Error: Book with ID {bookIdToBorrow} is already borrowed.");
                    return;
                }

                if (currentMember.BorrowBook(bookToBorrow, allLoans))
                {
                    Console.WriteLine($"Book with name '{bookToBorrow.BookName}' successfully borrowed.");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }

        // Return Book - Member Option
        public static void ReturnBook(Member currentMember, List<Loans> allLoans, List<Book> allbooks)
        {
            try
            {
                Console.WriteLine("Enter Book ID to Return: ");
                int bookIdToReturn = Convert.ToInt32(Console.ReadLine());

                bool bookBorrowedByMember = currentMember.BorrowedBooks.Any(book => book.BookId == bookIdToReturn);

                if (bookBorrowedByMember)
                {
                    currentMember.ReturnBook(bookIdToReturn, allLoans);
                    Console.WriteLine("\nBook returned successfully.");

                    Book bookToReturn = allbooks.FirstOrDefault(book => book.BookId == bookIdToReturn);
                    if (bookToReturn != null)
                    {
                        bookToReturn.Status = Book.BookStatus.Available;
                    }
                }
                else
                {
                    Console.WriteLine("You have not borrowed the book with the given ID.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for Book ID.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }




        // View Current Book Loans Details - Member and Librarin Option
        public static void ViewCurrentLoansDetails()
        {
            if (currentPerson is Member)
            {
                List<Loans> currentLoans = currentMember.ViewCurrentLoans(allLoans);
                if (currentLoans != null && currentLoans.Count > 0)
                {
                    Console.WriteLine("\nLoans/ Transactions ");
                    Console.WriteLine("\t{0, -20} {1, -20} {2, -12} {3, -12} {4, -12}", "Member Name", "Book Name", "Loan Date", "Due Date", "Return Date");

                    foreach (Loans loan in currentLoans)
                    {
                        DisplayLoanDetails(loan);
                    }
                }
                else
                {
                    Console.WriteLine("No books borrowed.");
                }
            }
            else if (currentPerson is Librarian)
            {
                List<Loans> currentLoans = currentLibrarian.ViewCurrentLoans(allLoans);
                if (currentLoans != null && currentLoans.Count > 0)
                {
                    Console.WriteLine("\nLoans/ Transactions");
                    Console.WriteLine("\t{0, -20} {1, -20} {2, -12} {3, -12} {4, -12}", "Member Name", "Book Name", "Loan Date", "Due Date", "Return Date");

                    foreach (Loans loan in currentLoans)
                    {
                        DisplayLoanDetails(loan);
                    }
                }
                else
                {
                    Console.WriteLine("No books borrowed.");
                }
            }
            else { Console.WriteLine("User not logged in, Login and try again.."); }

        }

        public static void DisplayLoanDetails(Loans loan)
        {
            Console.WriteLine(
                "\t{0, -20} {1, -20} {2, -12} {3, -12} {4, -12}",
                loan.Member.MemberName,
                loan.Book.BookName,
                loan.LoanDate.ToString("dd/MM/yyyy"),
                loan.DueDate.ToString("dd/MM/yyyy"),
                loan.ReturnDate);
        }

        // Borrowed Book - Member Option
        public static void ViewBorrowedBooks()
        {
            if (currentMember.BorrowedBooks.Count > 0)
            {
                Console.WriteLine("\n Borrowed Books:");
                Console.WriteLine("\t{0, -4} {1, -20} {2, -20} {3, -10}", "ID", "Name", "Author", "ISBN");

                foreach (Book book in currentMember.BorrowedBooks)
                {
                    DisplayBorrowedBookList(book);
                }
            }
            else
            {
                Console.WriteLine("No books borrowed.");
            }
        }

        public static void DisplayBorrowedBookList(Book book)
        {
            Console.WriteLine(
                "\t{0, -4} {1, -20} {2, -20} {3, -10}",
                book.BookId,
                book.BookName,
                book.Author,
                book.ISBN);
        }



        // Menu -  Option
        public static void Menu()
        {
            menuStack.Push(() => LibrarianOptionss());
            Console.WriteLine("Returning to the main menu...");
        }

        // Exit - Option
        public static void Logout()
        {
            menuStack.Clear();
            Main();
        }

        

    }
    


}


