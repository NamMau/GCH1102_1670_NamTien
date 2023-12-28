using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebAppDemo3.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(50, ErrorMessage = "Title must between 6 to 50 characters ", MinimumLength = 6)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Author")]
        [StringLength(30, ErrorMessage = "Author name must between 10 to 30 characters ", MinimumLength = 10)]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(3000, ErrorMessage = "Description must between 15 to 3000 characters ", MinimumLength = 15)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Image")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Published Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string PublishedDate { get; set; }

        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
