using BookSaw.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace BookSaw.Models
{
    public class Book:BaseEntity
    {
        [Required]
        public string ImgUrl { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; }
        public int Pages { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
