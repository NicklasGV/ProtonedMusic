using ProtonedMusicAPI.DTO.ImageDTO;

namespace ProtonedMusicAPI.Interfaces
{
    public interface IImageService
    {
        Task<Image> GetImageById(int id);
        Task<Image> AddImage(Image image);
        Task<Image> DeleteImage(int id);
    }
}

