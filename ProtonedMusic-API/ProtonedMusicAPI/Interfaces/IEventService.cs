namespace ProtonedMusicAPI.Interfaces
{
    public interface IEventService
    {
        Task<List<EventResponse>> GetAllEvents();
        Task<EventResponse> CreateEvent(EventRequest newEvent);
        Task<EventResponse?> FindByEventId(int eventId);
        Task<EventResponse?> UpdateEventById(int eventId, EventRequest updateEvent);
        Task<EventResponse?> DeleteEventById(int eventId);
        Task<EventResponse?> UploadEventPicture(int eventId, IFormFile file);
    }
}
