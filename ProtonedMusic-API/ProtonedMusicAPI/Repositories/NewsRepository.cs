namespace ProtonedMusicAPI.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly DatabaseContext _databaseContext;
        public NewsRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<News>> GetAllAsync()
        {
            return await _databaseContext.News
                .ToListAsync();
        }

        public async Task<News> FindByIdAsync(int newsId)
        {
            return await _databaseContext.News.FirstOrDefaultAsync(s => s.Id == newsId);
        }

        public async Task<News> CreateAsync(News newNews)
        {
            _databaseContext.News.Add(newNews);
            await _databaseContext.SaveChangesAsync();
            return newNews;
        }

        public async Task<News> DeleteByIdAsync(int newsId)
        {
            var news = await FindByIdAsync(newsId);

            if (news != null)
            {
                _databaseContext.Remove(news);
                await _databaseContext.SaveChangesAsync();
            }
            return news;
        }

        public async Task<News> UpdateByIdAsync(News updateNews)
        {
            News news = await FindByIdAsync(updateNews.Id);
            if (news != null)
            {
                news.Title = updateNews.Title;
                news.Text = updateNews.Text;
                news.DateTime = updateNews.DateTime;

                await _databaseContext.SaveChangesAsync();

                news = await FindByIdAsync(news.Id);
            }
            return news;
        }
    }
}
