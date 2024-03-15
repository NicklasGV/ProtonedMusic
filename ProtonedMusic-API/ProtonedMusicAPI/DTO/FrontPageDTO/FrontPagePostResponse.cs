namespace ProtonedMusicAPI.DTO.FrontpageDTO
{
    public class FrontpagePostResponse
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public Banner? Banner { get; set; }
        public string? FrontpagePicturePath { get; set; }
    }
}
