using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities
{
    [Index(nameof(Email), IsUnique=true)]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }


        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }


    }
}
