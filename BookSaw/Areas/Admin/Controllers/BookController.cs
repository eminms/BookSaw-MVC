using BookSaw.DAL;
using BookSaw.Models;
using BookSaw.Utilities.ImageUpload;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookSaw.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        BookDbContext _context;
        IWebHostEnvironment _env;
        public BookController(BookDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Book> books = await _context.Books.Include(b => b.Categories).ToListAsync();
            return View(books);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if(!book.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File must be image type");
                return View();
            }
            if (book.ImageFile.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("ImageFile", "Image size must be less than 2MB");
                return View();
            }
            //string path=Path.Combine(_env.WebRootPath,"Uploads/Book/");
            //string fileName=Guid.NewGuid().ToString()+ "_" +book.ImageFile.FileName;
            //string fullPath=Path.Combine(path,fileName);

            //using(FileStream stream=new FileStream(fullPath,FileMode.Create))
            //{
            //    await book.ImageFile.CopyToAsync(stream);
            //}

            var fileName= book.ImageFile.SaveImage(_env, "Uploads/Book/");
            book.ImgUrl=fileName;
            book.Categories = new List<Category>();

            if (!ModelState.IsValid) return View();
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Book book=await _context.Books.FirstOrDefaultAsync(b=>b.Id==id);
            if (book == null) return NotFound();
            book.ImgUrl.DeleteImage(_env, "Uploads/Product");
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }


        [HttpPost]
      public async Task<IActionResult> Update(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
            if (existingBook == null) return NotFound();
            existingBook.ImageFile = book.ImageFile;
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Description = book.Description;
            existingBook.Price = book.Price;
            existingBook.Publisher = book.Publisher;
            existingBook.ISBN = book.ISBN;
            existingBook.Language = book.Language;
            existingBook.Pages = book.Pages;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
