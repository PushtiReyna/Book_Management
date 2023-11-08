using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Management.Models;

public partial class BookMst
{
    public int BookId { get; set; }

    [Required(ErrorMessage = "Please enter book name")]
    [RegularExpression(@"^[a-zA-Z][a-zA-Z]+[a-zA-Z]$", ErrorMessage = "BookName must contain uppercase letter, lowercase letter.")]
    public string BookName { get; set; } = null!;

    [Required, Range(1, int.MaxValue, ErrorMessage = "Please Select Category.")]
    public int CategoryId { get; set; }

    [Required, Range(1, int.MaxValue, ErrorMessage = "Please Select Subcategory.")]
    public int SubcategoryId { get; set; }

    [Required(ErrorMessage = "Please name of Author")]
    [RegularExpression(@"^[a-zA-Z][a-zA-Z]+[a-zA-Z]$", ErrorMessage = "AuthorName must contain uppercase letter, lowercase letter.")]
    public string AuthorName { get; set; } = null!;

    [Required(ErrorMessage = "Please Enter book pages")]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    public int BookPages { get; set; }

    [Required(ErrorMessage = "Please Enter name of Publisher")]
    [RegularExpression(@"^[a-zA-Z][a-zA-Z]+[a-zA-Z]$", ErrorMessage = "Publisher must contain uppercase letter, lowercase letter.")]
    public string Publisher { get; set; } = null!;


    [DataType(DataType.DateTime)]
    [Required(ErrorMessage = "Please enter PublishDate")]
    public DateTime PublishDate { get; set; }


    [Required(ErrorMessage = "Please enter edition of book")]
    public string Edition { get; set; } = null!;


    [Required(ErrorMessage = "Please enter description")]
    [RegularExpression(@"^[a-zA-Z0-9][a-zA-Z0-9 ]+[a-zA-Z0-9]$", ErrorMessage = "Description must contain uppercase letter, lowercase letter and numbers.")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Please enter book's price")]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    public string Price { get; set; } = null!;

    [Required(ErrorMessage = "Please select cover Image")]
    public string CoverImagePath { get; set; } = null!;

    [Required(ErrorMessage = "Please select pdf of book")]
    public string PdfPath { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int UpdateBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    [NotMapped]
    public IFormFile Image { get; set; }

    [NotMapped]
    public IFormFile File { get; set; }

    [NotMapped]
    public string CategoryName { get; set; }
    [NotMapped]
    public string SubcategoryName { get; set; } = null!;
}
