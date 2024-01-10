namespace ProtonedMusicAPI.Interfaces.INews
{
    public interface INewsRepository
    {
        Task<List<News>> GetAllAsync();
        Task<News> CreateAsync(News newNews);
        Task<News?> FindByIdAsync(int newsId);
        Task<News?> UpdateByIdAsync(int newsId, News updateNews);
        Task<News?> DeleteByIdAsync(int newsId);
    }
}
