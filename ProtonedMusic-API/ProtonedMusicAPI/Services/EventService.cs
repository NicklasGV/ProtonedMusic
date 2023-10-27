using ProtonedMusicAPI.Database.Entities;

namespace ProtonedMusicAPI.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        private static EventResponse MapEventToEventResponse(Event events)
        {
            EventResponse response = new EventResponse
            {
                Id = events.Id,
                Title = events.Title,
                price = events.Price,
                Description = events.Description,
                Created = events.Created,
                TimeofEvent = events.TimeofEvent,
            };
            return response;
        }

        private static Event MapEventRequestToEvent(EventRequest eventRequest)
        {
            return new Event
            {
                Title = eventRequest.Title,
                Price = eventRequest.Price,
                Description = eventRequest.Description,
                Created = eventRequest.Created,
                TimeofEvent = eventRequest.TimeofEvent,
               // TimeofEvent = eventRequest.DateofEvent.ToDateTime(eventRequest.TimeofEvent),
            };
        }

        public async Task<EventResponse> CreateEvent(EventRequest newEvent)
        {
            var events = await _eventRepository.CreateEvent(MapEventRequestToEvent(newEvent));
            
            if (events == null)
            {
                throw new ArgumentNullException();
            }
            return MapEventToEventResponse(events);
        }

        public async Task<EventResponse?> DeleteEventById(int eventId)
        {
            var events = await _eventRepository.DeleteEventById(eventId);

            if (events != null)
            {
                return MapEventToEventResponse(events);
            }
            return null;
        }

        public async Task<EventResponse?> FindByEventId(int eventId)
        {
            var events = await _eventRepository.FindEventById(eventId);

            if (events != null)
            {
                return MapEventToEventResponse(events);
            }
            return null;
        }

        public async Task<List<EventResponse>> GetAllEvents()
        {
            List<Event> events = await _eventRepository.GetAllAsync();
            if (events == null)
            {
                throw new ArgumentNullException();
            }
            return events.Select(MapEventToEventResponse).ToList();
        }

        public async Task<EventResponse?> UpdateEventById(int eventId, EventRequest updateEvent)
        {
            var events = await _eventRepository.UpdateEventById(eventId, MapEventRequestToEvent(updateEvent));

            if(events != null)
            {
                return MapEventToEventResponse(events);
            }
            return null;
        }
    }
}
