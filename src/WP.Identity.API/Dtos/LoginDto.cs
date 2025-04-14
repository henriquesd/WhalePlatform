using System.ComponentModel.DataAnnotations;

namespace WP.Identity.API.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        [EmailAddress(ErrorMessage = "The field {0} is in an invalid format.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, ErrorMessage = "The field {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
        public required string Password { get; set; }
    }
}
