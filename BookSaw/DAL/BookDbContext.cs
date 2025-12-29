using BookSaw.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSaw.DAL
{
    public class BookDbContext: DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options): base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
