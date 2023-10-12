namespace ProtonedMusicAPI.DTO.ImageDTO
{
    public class ImageCreateResource
    {
        public Guid ImageId { get; set; }
        public string FileExtension { get; set; }
        public IFormFile ImageFile { get; set; }

        public int ProductId {  get; set; }
    }
}
