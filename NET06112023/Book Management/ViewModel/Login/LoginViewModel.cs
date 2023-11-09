using System.ComponentModel.DataAnnotations;

namespace Book_Management.ViewModel.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Enter username")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "invalid username.")]
        public string UserName { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Password.")]
        [MinLength(5, ErrorMessage = "invalid password ")]
        [MaxLength(8, ErrorMessage = "invalid password ")]
        public string Password { get; set; } = null!;
    }
}
