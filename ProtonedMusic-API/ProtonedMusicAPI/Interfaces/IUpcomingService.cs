namespace ProtonedMusicAPI.Interfaces
{
    public interface IUpcomingService
    {
        Task<List<UpcomingResponse>> GetAllUpcomings();
        Task<UpcomingResponse> CreateUpcoming(UpcomingRequest newUpcoming);
        Task<UpcomingResponse?> FindByUpcomingId(int upcomingId);
        Task<UpcomingResponse?> UpdateUpcomingById(int upcomingId, UpcomingRequest updateUpcoming);
        Task<UpcomingResponse?> DeleteUpcomingById(int upcomingId);
    }
}
