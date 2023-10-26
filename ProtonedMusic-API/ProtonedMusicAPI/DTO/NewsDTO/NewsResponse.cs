namespace ProtonedMusicAPI.DTO.NewsDTO
{
    public class NewsResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime DateTime { get; set; } = DateTime.Now;

        public List<NewsNewsLikeResponse> NewsLikes { get; set; } = new();
    }

    public class NewsNewsLikeResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Postal { get; set; }
    }
}
