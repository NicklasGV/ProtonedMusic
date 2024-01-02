namespace ProtonedMusicAPI.Database.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? FirstName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(50)")]
        public string? LastName { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Password { get; set; }

        public Role Role { get; set; }
        public AddonRoles AddonRoles { get; set; } = AddonRoles.None;

        [Column(TypeName = "int")]
        public int? PhoneNumber { get; set; } = int.MinValue;

        [Column(TypeName = "nvarchar(50)")]
        public string? Address { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(50)")]
        public string? City { get; set; } = string.Empty;

        [Column(TypeName = "int")]
        public int? Postal { get; set; } = int.MinValue;

        [Column(TypeName = "nvarchar(50)")]
        public string? Country { get; set; } = string.Empty;

        public string? ProfilePicturePath { get; set; }

        public List<NewsLike> NewsLikes { get; set; } = new List<NewsLike>();


    }
}
