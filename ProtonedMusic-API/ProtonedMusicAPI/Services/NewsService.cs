using ProtonedMusicAPI.Database.Entities;

namespace ProtonedMusicAPI.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;


        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public static NewsResponse MapNewsToNewsResponse(News news)
        {
            NewsResponse response = new NewsResponse
            {
                Id = news.Id,
                Title = news.Title,
                Text = news.Text,
                DateTime = news.DateTime,
            };
            if (news.NewsLikes.Count > 0)
            {
                response.NewsLikes = news.NewsLikes.Select(x => new NewsNewsLikeResponse
                {
                    Id = x.User.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email.ToLower(),
                    Role = x.User.Role,
                    PhoneNumber = x.User.PhoneNumber,
                    Address = x.User.Address,
                    Country = x.User.Country,
                    City = x.User.City,
                    Postal = x.User.Postal,
                }).ToList();
            }
            return response;
        }

        private static News MapNewsRequestToNews(NewsRequest newsRequest)
        {
            News news = new News
            {
                Title = newsRequest.Title,
                Text = newsRequest.Text,
                DateTime = newsRequest.DateTime,
                NewsLikes = newsRequest.UserIds.Select(u => new NewsLike
                {
                    user_Id = u
                }).ToList()
            };
            return news;
        }

        public async Task<List<NewsResponse>> GetAllAsync()
        {
            List<News> news = await _newsRepository.GetAllAsync();

            if (news == null)
            {
                throw new ArgumentException();
            }
            return news.Select(MapNewsToNewsResponse).ToList();
        }

        public async Task<NewsResponse> FindByIdAsync(int newsId)
        {
            var news = await _newsRepository.FindByIdAsync(newsId);

            if (news != null)
            {
                return MapNewsToNewsResponse(news);
            }

            return null;
        }

        public async Task<NewsResponse> CreateAsync(NewsRequest newNews)
        {
            var news = await _newsRepository.CreateAsync(MapNewsRequestToNews(newNews));
            if (news == null)
            {
                throw new ArgumentNullException();
            }
            return MapNewsToNewsResponse(news);
        }

        public async Task<NewsResponse> DeleteByIdAsync(int newsId)
        {
            var news = await _newsRepository.DeleteByIdAsync(newsId);

            if (news != null)
            {
                return MapNewsToNewsResponse(news);
            }
            return null;
        }

        public async Task<NewsResponse> UpdateByIdAsync(int newsId, NewsRequest updateNews)
        {
            var news = MapNewsRequestToNews(updateNews);
            var insertedNews = await _newsRepository.UpdateByIdAsync(newsId, news);

            if (insertedNews != null)
            {
                return MapNewsToNewsResponse(insertedNews);
            }

            return null;
        }
    }
}
