using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book_Management.Models;

public partial class CategoryMst
{
    [Required, Range(1, int.MaxValue, ErrorMessage = "Please Select Category.")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Please Enter CategoryName Name"), MaxLength(50)]
    public string CategoryName { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int UpdateBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
