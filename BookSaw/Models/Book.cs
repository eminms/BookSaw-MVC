using BookSaw.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSaw.Models
{
    public class Book:BaseEntity
    {

        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [Required(ErrorMessage ="Title is required")]
        [
            MaxLength(100,ErrorMessage ="Max length must be 100"),
            MinLength(5,ErrorMessage ="Min length must be 5")
        ]
        public string Title { get; set; }
        [Required(ErrorMessage ="Author is required")]
        [
            MaxLength(50, ErrorMessage = "Max length must be 50"),
            MinLength(2, ErrorMessage = "Min length must be 2")
        ]
        public string Author { get; set; }
        [Required(ErrorMessage ="Description is required")]
        [
            MaxLength(1000, ErrorMessage = "Max length must be 1000"),
            MinLength(10, ErrorMessage = "Min length must be 10")
        ]
        public string Description { get; set; }
        [Required(ErrorMessage ="Price is required")]
        [Range(1,1000,ErrorMessage ="Price must be between 1 and 1000")]
        public decimal Price { get; set; }
        [Required(ErrorMessage ="Publisher is required")]
        [
            MaxLength(50, ErrorMessage = "Max length must be 50"),
            MinLength(2, ErrorMessage = "Min length must be 2")
        ]
        public string Publisher { get; set; }
        [Required(ErrorMessage ="ISBN is required")]
        [
            MaxLength(13, ErrorMessage = "Max length must be 13"),
            MinLength(10, ErrorMessage = "Min length must be 10")
        ]
        public string ISBN { get; set; }
        [Required(ErrorMessage ="Language is required")]
        [
            MaxLength(30, ErrorMessage = "Max length must be 30"),
            MinLength(2, ErrorMessage = "Min length must be 2")
        ]
        public string Language { get; set; }
        [Required(ErrorMessage ="Pages is required")]
        [Range(1,5000,ErrorMessage ="Pages must be between 1 and 5000")]
        public int Pages { get; set; }
        [Required(ErrorMessage ="Categories are required")]
        [MinLength(1,ErrorMessage ="At least one category must be selected")]
        public List<Category>? Categories { get; set; }
    }
}
