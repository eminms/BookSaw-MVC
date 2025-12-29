using BookSaw.DAL;
using BookSaw.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookSaw.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        BookDbContext _context;
        public BookController(BookDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Book> books = _context.Books.Include(b => b.Categories).ToList();
            return View(books);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existBook = _context.Books.FirstOrDefault(b => b.Id == book.Id);
            if (existBook == null) return NotFound();
            existBook.ImgUrl = book.ImgUrl;
            existBook.Title = book.Title;
            existBook.Author = book.Author;
            existBook.Description = book.Description;
            existBook.Price = book.Price;
            existBook.Publisher = book.Publisher;
            existBook.ISBN = book.ISBN;
            existBook.Language = book.Language;
            existBook.Pages = book.Pages;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
