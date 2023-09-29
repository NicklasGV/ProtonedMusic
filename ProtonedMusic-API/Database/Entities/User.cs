namespace ProtonedMusic_API.Database.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public  string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [ForeignKey("Login.Email")]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        public Login login { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string City { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Postal { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Country { get; set; }

    }
}
