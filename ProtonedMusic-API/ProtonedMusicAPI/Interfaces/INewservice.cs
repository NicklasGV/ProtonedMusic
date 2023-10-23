namespace ProtonedMusicAPI.Interfaces
{
    public interface INewsService
    {
        Task<List<NewsResponse>> GetAllAsync();
        Task<NewsResponse> CreateAsync(NewsRequest newNews);
        Task<NewsResponse?> FindByIdAsync(int newsId);
        Task<NewsResponse?> UpdateByIdAsync(NewsRequest newNews);
        Task<NewsResponse?> DeleteByIdAsync(int newsId);
    }
}
