using Microsoft.Extensions.Logging;

namespace ProtonedMusicAPI.Repositories
{
    public class UpcomingRepository : IUpcomingRepository
    {
        private readonly DatabaseContext _context;

        public UpcomingRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Upcoming> CreateUpcoming(Upcoming newUpcoming)
        {
            _context.upcomings.Add(newUpcoming);

            await _context.SaveChangesAsync();
            newUpcoming = await FindUpcomingById(newUpcoming.Id);
            return newUpcoming;
        }

        public async Task<Upcoming?> DeleteUpcomingById(int upcomingId)
        {
            var upcomings = await FindUpcomingById(upcomingId);

            if (upcomings != null)
            {
                _context.Remove(upcomings);
                await _context.SaveChangesAsync();
            }
            return upcomings;
        }

        public async Task<Upcoming?> FindUpcomingById(int upcomingId)
        {
            return await _context.upcomings.FirstOrDefaultAsync(u => u.Id == upcomingId);
        }

        public async Task<List<Upcoming>> GetAllAsync()
        {
            return await _context.upcomings.ToListAsync();
        }

        public async Task<Upcoming?> UpdateUpcomingById(int upcomingId, Upcoming updateUpcoming)
        {
            Upcoming upcomings = await FindUpcomingById(upcomingId);
            if (upcomings != null)
            {
                upcomings.Title = updateUpcoming.Title;
                upcomings.Description = updateUpcoming.Description;
                upcomings.timeOf = updateUpcoming.timeOf;

                await _context.SaveChangesAsync();
                upcomings = await FindUpcomingById(upcomingId);
            }
            return upcomings;
        }
    }
}
