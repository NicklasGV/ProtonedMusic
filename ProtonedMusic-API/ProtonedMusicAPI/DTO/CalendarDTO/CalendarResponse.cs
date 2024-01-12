namespace ProtonedMusicAPI.DTO.CalendarDTO
{
    public class CalendarResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public ArtistCalendarResponse Artist { get; set; }
    }

    public class ArtistCalendarResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
