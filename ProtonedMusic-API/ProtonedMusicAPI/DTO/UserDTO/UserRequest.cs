namespace ProtonedMusicAPI.DTO.UserDTO
{
    public class UserRequest
    {
        [StringLength(32, ErrorMessage = "First name cannot be longer than 32 chars")]
        public string FirstName { get; set; }

        [StringLength(32, ErrorMessage = "Last name cannot be longer than 32 chars")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Email cannot be longer than 50 chars")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Password cannot be longer than 50 chars")]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }

        public int PhoneNumber { get; set; }

        [StringLength(50, ErrorMessage = "Address line one cannot be longer than 50 chars")]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "City cannot be longer than 50 chars")]
        public string City { get; set; }
        public int Postal { get; set; }

        [StringLength(40, ErrorMessage = "Country cannot be longer than 40 chars")]
        public string Country { get; set; }

        public string ProfilePicturePath { get; set; }

        public List<int> NewsIds { get; set; } = new();
    }
}
