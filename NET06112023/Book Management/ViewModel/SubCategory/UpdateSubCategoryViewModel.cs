using System.ComponentModel.DataAnnotations;

namespace Book_Management.ViewModel.SubCategory
{
    public class UpdateSubCategoryViewModel
    {
        public int SubcategoryId { get; set; }

        [Required(ErrorMessage = "Please Enter CategoryName Name"), MaxLength(10)]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Please enter only letters for SubcategoryName.")]
        public string SubcategoryName { get; set; } = null!;

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please Select category.")]
        public int CategoryId { get; set; }
    }
}
