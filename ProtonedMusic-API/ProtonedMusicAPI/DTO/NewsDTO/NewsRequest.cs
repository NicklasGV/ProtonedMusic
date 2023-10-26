namespace ProtonedMusicAPI.DTO.NewsDTO
{
    public class NewsRequest
    {
        [Required]
        [StringLength(80, ErrorMessage = "Product Name cannot be longer than 80 characters")]
        public string Title { get; set; }

        [Required]
        [StringLength(600, ErrorMessage = "Text cannot be longer than 500 characters")]
        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public List<int> UserIds { get; set; } = new();
    }
}
