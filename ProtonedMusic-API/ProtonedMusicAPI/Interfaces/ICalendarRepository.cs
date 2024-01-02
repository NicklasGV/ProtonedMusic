using System.Globalization;

namespace ProtonedMusicAPI.Interfaces
{
    public interface ICalendarRepository
    {
        Task<List<CalendarContent>> GetAllAsync();
        Task<CalendarContent> CreateCalendar(CalendarContent newCalendar);
        Task<CalendarContent?> FindCalendarById(int calendarId);
        Task<CalendarContent?> UpdateCalendarById(int calendarId, CalendarContent updateCalendar);
        Task<CalendarContent?> DeleteCalendarById(int calendarId);
    }
}
