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
        public async Task<IActionResult> Index()
        {
            List<Book> books = await _context.Books.Include(b=>b.Categories).ToListAsync();
            return View(books);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var book = await _context.Books.Include(b => b.Categories).FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }
    }
}
