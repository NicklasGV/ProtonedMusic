namespace ProtonedMusicAPI.Database.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Password { get; set; }

        public Role Role { get; set; }

        [Column(TypeName = "int")]
        public int PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string City { get; set; }

        [Column(TypeName = "int")]
        public int Postal { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Country { get; set; }

        public string? ProfilePicturePath { get; set; }

        public List<NewsLike> NewsLikes { get; set; } = new List<NewsLike>();

        public AddonRoles AddonRoles { get; set; }

    }
}
