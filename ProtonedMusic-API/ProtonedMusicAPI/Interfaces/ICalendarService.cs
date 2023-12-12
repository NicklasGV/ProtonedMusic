using ProtonedMusicAPI.DTO.CalendarDTO;

namespace ProtonedMusicAPI.Interfaces
{
    public interface ICalendarService
    {
        Task<List<CalendarResponse>> GetAllAsync();
        Task<CalendarResponse> CreateAsync(CalendarRequest newCalendar);
        Task<CalendarResponse?> FindByIdAsync(int calendarId);
        Task<CalendarResponse?> UpdateByIdAsync(int calendarId, CalendarRequest updateCalendar);
        Task<CalendarResponse?> DeleteByIdAsync(int calendarId);
    }
}
