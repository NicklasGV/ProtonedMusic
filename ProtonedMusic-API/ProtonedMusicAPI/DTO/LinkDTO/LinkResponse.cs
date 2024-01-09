namespace ProtonedMusicAPI.DTO.LinkDTO
{
    public class LinkResponse
    {
        public int Id { get; set; }
        public LinkArtistResponse Artist {  get; set; }
        public string Title { get; set; } = string.Empty;

        public string LinkAddress { get; set; } = string.Empty;
    }
    public class LinkArtistResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string? PicturePath { get; set; }
    }

}
