using ProtonedMusicAPI.DTO.CalendarDTO;

namespace ProtonedMusicAPI.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ICalendarRepository _calendarRepository;
        public CalendarService(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
        }

        private static CalendarResponse MapContentToCalendarResponse(CalendarContent contents)
        {
            CalendarResponse response = new CalendarResponse
            {
                Id = contents.Id,
                Title = contents.Title,
                Content = contents.Content,
                Date = contents.Date,
                FamilyMember = contents.FamilyMember,
            };
            return response;
        }

        private static CalendarContent MapCalendarRequestToContent(CalendarRequest calendarRequest)
        {
            return new CalendarContent
            {
                Title = calendarRequest.Title,
                Content = calendarRequest.Content,
                Date = calendarRequest.Date,
                FamilyMember = calendarRequest.FamilyMember
            };
        }
        public async Task<CalendarResponse> CreateAsync(CalendarRequest newCalendar)
        {
            var content = await _calendarRepository.CreateCalendar(MapCalendarRequestToContent(newCalendar));

            if (content == null)
            {
                throw new ArgumentNullException();
            }
            return MapContentToCalendarResponse(content);
        }

        public async Task<CalendarResponse?> DeleteByIdAsync(int calendarId)
        {
            var content = await _calendarRepository.DeleteCalendarById(calendarId);

            if (content != null)
            {
                return MapContentToCalendarResponse(content);
            }
            return null;
        }

        public async Task<CalendarResponse?> FindByIdAsync(int calendarId)
        {
            var content = await _calendarRepository.FindCalendarById(calendarId);

            if (content != null)
            {
                return MapContentToCalendarResponse(content);
            }
            return null;
        }

        public async Task<List<CalendarResponse>> GetAllAsync()
        {
            List<CalendarContent> content = await _calendarRepository.GetAllAsync();

            if (content == null)
            {
                throw new ArgumentNullException();
            }
            return content.Select(MapContentToCalendarResponse).ToList();
        }

        public async Task<CalendarResponse?> UpdateByIdAsync(int calendarId, CalendarRequest updateCalendar)
        {
            var content = await _calendarRepository.UpdateCalendarById(calendarId, MapCalendarRequestToContent(updateCalendar));

            if (content != null)
            {
                return MapContentToCalendarResponse(content);
            }
            return null;
        }
    }
}
