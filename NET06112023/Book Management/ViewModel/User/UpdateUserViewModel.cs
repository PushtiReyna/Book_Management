﻿using System.ComponentModel.DataAnnotations;

namespace Book_Management.ViewModel.User
{
    public class UpdateUserViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please Enter Fullname."), MaxLength(30)]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "invalid fullname")]
        public string FullName { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please Enter Email.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter username"),MaxLength(10)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "invalid username.")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter Address"), MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "invalid address")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Please Enter ContactNumber")]
        [MinLength(10, ErrorMessage = "invalid contact number")]
        [MaxLength(10, ErrorMessage = "invalid contact number")]
        public string ContactNumber { get; set; } = null!;
    }
}
