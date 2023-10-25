using ProtonedMusicAPI.DTO.ImageDTO;

namespace ProtonedMusicAPI.Interfaces
{
    public interface IImageService
    {
        Task<List<ImageResponse>> GetAll();
        Task<ImageResponse> Create(ImageRequest newImage);
        Task<ImageResponse?> FindById(Guid ImageId);
        Task<ImageResponse?> DeleteById(int ImageId);
    }
}

