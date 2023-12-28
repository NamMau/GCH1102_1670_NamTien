using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebAppDemo3.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        [StringLength(15, ErrorMessage = "Category Name must between 6 to 15 characters ", MinimumLength = 6)]
        public string? CategoryName { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Description must between 15 to 500 characters ", MinimumLength = 15)]
        public string Description { get; set; }
        public virtual ICollection<Book>? Book { get; set; }
    }
}
