using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProtonedMusic.Utility.Models
{
    public class LoginModel
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
