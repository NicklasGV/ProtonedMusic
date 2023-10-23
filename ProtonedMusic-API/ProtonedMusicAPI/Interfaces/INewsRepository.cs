namespace ProtonedMusicAPI.Interfaces
{
    public interface INewsRepository
    {
        Task<List<News>> GetAllAsync();
        Task<News> CreateAsync(News newNews);
        Task<News?> FindByIdAsync(int newsId);
        Task<News?> UpdateByIdAsync(News updateNews);
        Task<News?> DeleteByIdAsync(int newsId);
    }
}
