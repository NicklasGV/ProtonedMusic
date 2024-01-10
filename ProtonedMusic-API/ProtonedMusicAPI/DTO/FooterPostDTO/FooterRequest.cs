namespace ProtonedMusicAPI.DTO.FooterPostDTO
{
    public class FooterRequest
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        public string AddressMapLink { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Phonenumber { get; set; }
    }
}
