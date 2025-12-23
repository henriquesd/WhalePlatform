using System.ComponentModel.DataAnnotations;

namespace WP.Web.Client.Models
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
    }
}
