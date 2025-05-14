using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.Data.Entities
{
    public class User
    {
            [Key]
            public int Id { get; set; }

            [Required]
            [MaxLength(50)]
            public string Username { get; set; } = string.Empty;

            [Required]
            [MaxLength(50)]
            public string Password { get; set; } = string.Empty;  

            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            [Phone]
            public string PhoneNumber { get; set; } = string.Empty;

            public ICollection<Order>? Orders { get; set; }
        
    }
}
