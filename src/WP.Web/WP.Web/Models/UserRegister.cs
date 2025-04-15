using System.ComponentModel.DataAnnotations;

namespace WP.Web.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        [EmailAddress(ErrorMessage = "The field {0} is in an invalid format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, ErrorMessage = "The field {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string PasswordConfirmation { get; set; }
    }
}
