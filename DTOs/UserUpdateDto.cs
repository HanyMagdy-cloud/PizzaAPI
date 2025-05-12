using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.DTOs
{
    public class UserUpdateDto
    {
        [Required]
        public string Email { get; set; } = "";

        [Required]
        public string PhoneNumber { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        [Required]

        public string Username { get; set; } = "";

    }
}
