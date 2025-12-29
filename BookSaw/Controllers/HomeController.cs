using System.Diagnostics;
using BookSaw.DAL;
using BookSaw.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookSaw.Controllers
{
    public class HomeController : Controller
    {
        BookDbContext _context;
        public HomeController(BookDbContext context)
        {
            _context = context;
        }
        public  IActionResult Index()
        {
            List<Book> books = _context.Books.Include(b=>b.Categories).ToList();
            return View(books);
        }
        public IActionResult Detail(int id)
        {
            var book=_context.Books.Include(b=>b.Categories).FirstOrDefault(b=>b.Id==id);
            if (book == null) return NotFound();
            return View(book);
        }
    }
}
