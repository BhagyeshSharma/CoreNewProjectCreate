using ClassDAL;
using Data;
using Data.Migrations;
using Entity.Modal;
using InfraStucture.Contract;
using Microsoft.AspNetCore.Mvc;
using NewCoreApp.Models;
using Microsoft.EntityFrameworkCore;
using Entity.ViewModal;
namespace NewCoreApp.Controllers
{
    public class LibraryBooksController : Controller
    {
        private readonly UserMgMtContext _context;  
        private readonly IUnitOfWork _unitOfWork;   
        private readonly dbRepository _dbOperations;    
        public LibraryBooksController(UserMgMtContext context,IUnitOfWork unitOfWork,dbRepository dbRepository)
        {
            _context = context; 
            _unitOfWork = unitOfWork;   
            _dbOperations = dbRepository;   
        }
        public IActionResult Librarybooks()
        {
            var studentId = 1; // Replace this with the actual student ID you want to use

            var books = _context.Tbl_Books
                .Select(book => new BookTransactionViewModel
                {
                    Book_Id = book.Book_Id,
                    Title = book.BookTitle,
                    Author = book.BookAuthor,
                    IsAvailable = book.IsAvailable,
                    // Fetching transaction details if book is currently borrowed
                    TransactionId = _context.Tbl_BookTransaction
                        .Where(t => t.BookId == book.Book_Id && t.ReturnDate == null)
                        .Select(t => t.BookTransactionId)
                        .FirstOrDefault(),
                    BorrowedDate = _context.Tbl_BookTransaction
                        .Where(t => t.BookId == book.Book_Id && t.ReturnDate == null)
                        .Select(t => t.BorrowedDate)
                        .FirstOrDefault(),
                    ReturnDate = _context.Tbl_BookTransaction
                        .Where(t => t.BookId == book.Book_Id && t.ReturnDate == null)
                        .Select(t => t.ReturnDate)
                        .FirstOrDefault(),
                    PenaltyAmount = _context.Tbl_BookTransaction
                        .Where(t => t.BookId == book.Book_Id && t.ReturnDate == null)
                        .Select(t => t.PenaltyAmount)
                        .FirstOrDefault(),
                    StudentId = studentId // Replace this with the actual student's ID
                })
                .ToList();
            return View(books);
        }
        // Borrow a book
        [HttpPost]
        public IActionResult BorrowBook(int bookId, int studentId)
        {
            var book = _context.Tbl_Books.Find(bookId);
            if (book != null && book.IsAvailable)
            {
                var transaction = new Entity.Modal.Tbl_BookTransaction
                {
                    StudentId = studentId,
                    BookId = bookId,
                    BorrowedDate = DateTime.Now,
                    PenaltyAmount = 0
                };
                book.IsAvailable = false;  // Mark book as borrowed
                _context.Tbl_BookTransaction.Add(transaction);
                _context.SaveChanges();
                TempData["Message"] = "You have successfully borrowed the book!";
            }
            else
            {
                TempData["Message"] = "Sorry, the book is not available!";
            }
            return RedirectToAction("Librarybooks");
            
        }

        // Return a book
        [HttpPost]
        public IActionResult ReturnBook(int transactionId)
        {
            var transaction = (from t in _context.Tbl_BookTransaction
                               join b in _context.Tbl_Books on t.BookId equals b.Book_Id
                               where t.BookTransactionId == transactionId
                               select new
                               {
                                   t.BookTransactionId,
                                   t.StudentId,
                                   t.BookId,
                                   t.BorrowedDate,
                                   t.ReturnDate,
                                   t.PenaltyAmount,
                                   b.IsAvailable // Joined Book's availability status
                               }).FirstOrDefault();

            if (transaction != null)
            {
                // Update ReturnDate and Book availability status
                var dbTransaction = _context.Tbl_BookTransaction.FirstOrDefault(t => t.BookTransactionId == transaction.BookTransactionId);
                if (dbTransaction != null)
                {
                    dbTransaction.ReturnDate = DateTime.Now;
                    var overdueDays = (dbTransaction.ReturnDate.Value - transaction.BorrowedDate).Days - 14; // Example: 14 din tak penalty-free
                    if (overdueDays > 0)
                    {
                        dbTransaction.PenaltyAmount = overdueDays * 5; // 5 units per day penalty
                    }
                    // Manually update the availability status in Book
                    var book = _context.Tbl_Books.FirstOrDefault(b => b.Book_Id == transaction.BookId);
                    if (book != null)
                    {
                        book.IsAvailable = true;  // Mark the book as available
                    }
                    _context.SaveChanges();  // Save changes to the database
                   
                }
                TempData["Message"] = "You have successfully returned the book!";
            }
            else
            {
                TempData["Message"] = "Transaction not found!";
            }
            return RedirectToAction("Librarybooks");
        }

        // Show book availability status
        public IActionResult BookAvailability(int bookId)
        {
            var book = _context.Tbl_Books.Find(bookId);
            if (book != null)
            {
                return Json(new { isAvailable = book.IsAvailable });
            }
            return NotFound();
        }
    }
}
