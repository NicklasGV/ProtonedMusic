namespace ProtonedMusicAPI.DTO.CalendarDTO
{
    public class CalendarRequest
    {
        [Required]
        [StringLength(80, ErrorMessage = "Content Title cannot be longer than 80 characters")]
        public string Title { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Content cannot be longer than 255 characters")]
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public int ArtistId { get; set; }
    } 
}
