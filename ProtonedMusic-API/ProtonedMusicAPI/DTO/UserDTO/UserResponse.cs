namespace ProtonedMusicAPI.DTO.UserDTO
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public int PhoneNumber { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Postal { get; set; }
    }
}
