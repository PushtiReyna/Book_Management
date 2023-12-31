﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Management.Models;

public partial class SubcategoryMst
{

    public int SubcategoryId { get; set; }

    [Required(ErrorMessage = "Please Enter CategoryName Name")]
    [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Please enter only letters for SubcategoryName.")]
    public string SubcategoryName { get; set; } = null!;

    [Required, Range(1, int.MaxValue, ErrorMessage = "Please Select category.")]
    public int CategoryId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int UpdateBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

}
