using System.ComponentModel.DataAnnotations;

namespace authentication_autharization.Models
{
    public class LoginModel
    {
        [Required (ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
