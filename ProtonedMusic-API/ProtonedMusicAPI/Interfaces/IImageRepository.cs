using Microsoft.Extensions.Hosting;

namespace ProtonedMusicAPI.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> GetImageById(int id);
        Task<Image> Add(Image image);
        Task<Image> DeleteImage(int id);
    }
}
