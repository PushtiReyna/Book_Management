using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Book_Management.ViewModel.SubCategory
{
    public class GetSubCategoryViewModel
    {
        public int SubcategoryId { get; set; }

        public string SubcategoryName { get; set; } = null!;

        public string CategoryName { get; set; }
    }
}
