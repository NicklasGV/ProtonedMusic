using Org.BouncyCastle.Bcpg;
using ProtonedMusicAPI.Interfaces.ICalendar;

namespace ProtonedMusicAPI.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly DatabaseContext _context;

        public CalendarRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<CalendarContent> CreateCalendar(CalendarContent newCalendar)
        {
            _context.CalendarContent.Add(newCalendar);

            await _context.SaveChangesAsync();
            newCalendar = await FindCalendarById(newCalendar.Id);
            return newCalendar;
        }

        public async Task<CalendarContent?> DeleteCalendarById(int calendarId)
        {
            var content = await FindCalendarById(calendarId);

            if (content != null)
            {
                _context.Remove(content);
                await _context.SaveChangesAsync();
            }
            return content;
        }

        public async Task<CalendarContent?> FindCalendarById(int calendarId)
        {
            return await _context.CalendarContent.FirstOrDefaultAsync(c => c.Id == calendarId);
        }

        public async Task<List<CalendarContent>> GetAllAsync()
        {
            return await _context.CalendarContent.ToListAsync();
        }

        public async Task<CalendarContent?> UpdateCalendarById(int calendarId, CalendarContent updateCalendar)
        {
            CalendarContent contents = await FindCalendarById(calendarId);

            if (contents != null)
            {
                contents.Title = updateCalendar.Title;
                contents.Content = updateCalendar.Content;
                contents.Date = updateCalendar.Date;
                contents.FamilyMember = updateCalendar.FamilyMember;

                await _context.SaveChangesAsync();
                contents = await FindCalendarById(calendarId);
            }
            return contents;
        }
    }
}
