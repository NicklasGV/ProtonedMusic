namespace ProtonedMusicAPI.Database.Entities
{
    public class FooterPost
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string AddressMapLink { get; set; }
        public string Mail { get; set; }
        public string Phonenumber { get; set; }
    }
}
