using System.ComponentModel.DataAnnotations;

namespace WP.Web.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        [EmailAddress(ErrorMessage = "The field {0} is in an invalid format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public string Password { get; set; }
    }
}
