using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Book_Management.ViewModel.Book
{
    public class AddBookViewModel
    {

        [Required(ErrorMessage = "Please enter book name")]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "invalid BookName.")]
        public string BookName { get; set; } = null!;

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please Select Category.")]
        public int CategoryId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please Select Subcategory.")]
        public int SubcategoryId { get; set; }

        [Required(ErrorMessage = "Please name of Author")]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "invalid Author Name.")]
        public string AuthorName { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter book pages")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int BookPages { get; set; }

        [Required(ErrorMessage = "Please Enter name of Publisher")]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "invalid Publisher's Name.")]
        public string Publisher { get; set; } = null!;


        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Please enter PublishDate")]
        public DateTime PublishDate { get; set; }


        [Required(ErrorMessage = "Please enter edition of book")]
        public string Edition { get; set; } = null!;


        [Required(ErrorMessage = "Please enter description")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "invalid description")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Please enter book's price")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [DataType(DataType.Currency)]
        public string Price { get; set; } = null!;

        [Required(ErrorMessage = "Please select cover Image")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Please select pdf of book")]
        public IFormFile File { get; set; }
    }

}

