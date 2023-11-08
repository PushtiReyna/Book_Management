using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book_Management.Models;

public partial class UserMst
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Please Enter Fullname.")]
    [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Please enter only letters for FullName.")]
    public string FullName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Please Enter Email.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Please Enter username")]
    [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "username must contain uppercase letter,lowercase letter and special chararcters.")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Please Enter Address")]
    [RegularExpression(@"^[a-zA-Z0-9][a-zA-Z0-9]+[a-zA-Z0-9]$", ErrorMessage = "Address must contain uppercase letter, lowercase letter and numbers.")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "Please Enter ContactNumber")]
    [MinLength(10, ErrorMessage = "The ContactNumber must be at least 10 characters")]
    [MaxLength(10, ErrorMessage = "The ContactNumber cannot be more than 10 characters")]
    public string ContactNumber { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please Enter Password.")]
    [MinLength(5, ErrorMessage = "The password must be at least 5 characters")]
    [MaxLength(8, ErrorMessage = "The password cannot be more than 8 characters")]
    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int UpdateBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
