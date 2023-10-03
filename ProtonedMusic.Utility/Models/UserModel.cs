using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProtonedMusic.Utility.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Postal { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
