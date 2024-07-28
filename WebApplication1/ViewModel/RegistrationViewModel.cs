using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModel
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Please Enter Valid Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}
