using System.ComponentModel.DataAnnotations;

namespace AECPrototype.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "{0} must be between {2} and {1} characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public Boolean RememberMe { get; set; }

    }
}
