using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.Data.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; } 

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; } 

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
