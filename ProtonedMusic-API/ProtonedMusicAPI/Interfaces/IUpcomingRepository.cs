namespace ProtonedMusicAPI.Interfaces
{
    public interface IUpcomingRepository
    {
        Task<List<Upcoming>> GetAllAsync();
        Task<Upcoming> CreateUpcoming(Upcoming newUpcoming);
        Task<Upcoming?> FindUpcomingById(int upcomingId);
        Task<Upcoming?> UpdateUpcomingById(int upcomingId, Upcoming updateUpcoming);
        Task<Upcoming?> DeleteUpcomingById(int upcomingId);
    }
}
