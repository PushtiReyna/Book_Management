using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Management.ViewModel
{
    public class BookViewModel
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Please Enter Book Name")]
        public string BookName { get; set; } = null!;

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please Select Category.")]
        public int CategoryId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please Select Subcategory.")]
        public int SubcategoryId { get; set; }

        [Required(ErrorMessage = "Please name of Author")]
        public string AuthorName { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter First Name")]
        public int BookPages { get; set; }

        [Required(ErrorMessage = "Please Enter name of Publisher")]
        public string Publisher { get; set; } = null!;

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Please enter PublishDate")]
        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = "Please enter edition of book")]
        public string Edition { get; set; } = null!;

        [Required(ErrorMessage = "Please enter description")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Please enter book's price")]
        public string Price { get; set; } = null!;

        [Required(ErrorMessage = "Please select cover Image")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Please select pdf of book")]
        public IFormFile File { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int UpdateBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        
    }
}
