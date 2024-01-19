namespace ProtonedMusicAPI.DTO.LinkDTO
{
    public class LinkRequest
    {
        public List<int> ArtistIds { get; set; } = new();
        public string Title { get; set; }
        public string LinkAddress { get; set; }
    }
}
