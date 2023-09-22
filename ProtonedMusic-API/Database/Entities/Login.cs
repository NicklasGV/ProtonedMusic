namespace ProtonedMusic_API.Database.Entities
{
    public class Login
    {
        [Key]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }
    }
}
