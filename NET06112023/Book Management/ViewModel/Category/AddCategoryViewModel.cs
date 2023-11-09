using System.ComponentModel.DataAnnotations;

namespace Book_Management.ViewModel.Category
{
    public class AddCategoryViewModel
    {

        [Required(ErrorMessage = "Please Enter CategoryName Name"), MaxLength(10)]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "invalid categoryName.")]
        public string CategoryName { get; set; } = null!;

    }
}
