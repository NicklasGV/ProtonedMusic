namespace ProtonedMusicAPI.Interfaces
{
    public interface IImageRepository
    {
        Task<List<Image>> GetAll();
        Task<Image?> FindById(int Id);
        Task<Image> CreateImage(Image createImage);
        Task<Image?> UpdateImage(Image updateImage);
        Task<Image?> DeleteImageById(int ImageId);
    }
}
