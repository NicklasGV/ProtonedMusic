using ProtonedMusicAPI.DTO.ImageDTO;

namespace ProtonedMusicAPI.Interfaces
{
    public interface IImageService
    {
        Task<List<Image>> GetAll();
        Task<Image?> FindById(int Id);
        Task<Image> CreateImage(Image createImage);
        Task<Image?> UpdateImage(Image updateImage);
        Task<Image?> DeleteImageById(int ImageId);

        Task<Image?> UploadImage(ImageRequest imageRequest, IFormFile imageFile);
    }
}

