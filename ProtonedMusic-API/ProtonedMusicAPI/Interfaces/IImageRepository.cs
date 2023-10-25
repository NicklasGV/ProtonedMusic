namespace ProtonedMusicAPI.Interfaces
{
    public interface IImageRepository
    {
        Task<List<Category>> GetAll();
        Task<Image> Create(Image newImage);
        Task<Image?> FindById(Guid ImageId);
        Task<Image?> DeleteById(int deleteImageId);
    }
}
