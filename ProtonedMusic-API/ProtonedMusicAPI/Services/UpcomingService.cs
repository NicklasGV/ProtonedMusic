using Microsoft.Extensions.Logging;

namespace ProtonedMusicAPI.Services
{
    public class UpcomingService : IUpcomingService
    {
        private readonly IUpcomingRepository _repository;
        public UpcomingService(IUpcomingRepository upcomingRepository)
        {
            _repository = upcomingRepository;
        }

        private static UpcomingResponse MapUpcomingToUpcomingResponse(Upcoming upcomings)
        {
            UpcomingResponse response = new UpcomingResponse
            {
                Id = upcomings.Id,
                Title = upcomings.Title,
                Description = upcomings.Description,
                Created = upcomings.Created,
                Timeof = upcomings.timeOf,
            };
            return response;
        }

        private static Upcoming MapUpcomingRequestToUpcoming(UpcomingRequest upcomingRequest)
        {
            return new Upcoming
            {
                Title = upcomingRequest.Title,
                Description = upcomingRequest.Description,
                Created = upcomingRequest.Created,
                timeOf = upcomingRequest.Timeof,
            };
        }
        public async Task<UpcomingResponse> CreateUpcoming(UpcomingRequest newUpcoming)
        {
            var upcomings = await _repository.CreateUpcoming(MapUpcomingRequestToUpcoming(newUpcoming));

            if (upcomings == null)
            {
                throw new ArgumentNullException();
            }
            return MapUpcomingToUpcomingResponse(upcomings);
        }
        public async Task<UpcomingResponse?> DeleteUpcomingById(int upcomingId)
        {
            var upcomings = await _repository.DeleteUpcomingById(upcomingId);

            if (upcomings != null)
            {
                return MapUpcomingToUpcomingResponse(upcomings);
            }
            return null;
        }
        public async Task<UpcomingResponse?> FindByUpcomingId(int upcomingId)
        {
            var upcomings = await _repository.FindUpcomingById(upcomingId);

            if (upcomings != null)
            {
                return MapUpcomingToUpcomingResponse(upcomings);
            }
            return null;
        }
        public async Task<List<UpcomingResponse>> GetAllUpcomings()
        {
            List<Upcoming> events = await _repository.GetAllAsync();
            if (events == null)
            {
                throw new ArgumentNullException();
            }
            return events.Select(MapUpcomingToUpcomingResponse).ToList();
        }
        public async Task<UpcomingResponse?> UpdateUpcomingById(int upcomingId, UpcomingRequest updateUpcoming)
        {
            var upcomings = await _repository.UpdateUpcomingById(upcomingId, MapUpcomingRequestToUpcoming(updateUpcoming));

            if (upcomings != null)
            {
                return MapUpcomingToUpcomingResponse(upcomings);
            }
            return null;
        }
    }
}
