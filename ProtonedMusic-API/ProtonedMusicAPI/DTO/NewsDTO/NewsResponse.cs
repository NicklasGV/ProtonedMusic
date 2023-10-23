namespace ProtonedMusicAPI.DTO.NewsDTO
{
    public class NewsResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
