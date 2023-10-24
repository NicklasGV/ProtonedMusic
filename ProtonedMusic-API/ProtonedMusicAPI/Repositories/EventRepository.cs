using Microsoft.EntityFrameworkCore.Diagnostics;
using ProtonedMusicAPI.Database.Entities;
using ProtonedMusicAPI.Database;

namespace ProtonedMusicAPI.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly DatabaseContext _context;

        public EventRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Event> CreateEvent(Event newEvent)
        {
            _context.Events.Add(newEvent);

            await _context.SaveChangesAsync();
            newEvent = await FindEventById(newEvent.Id);
            return newEvent;
        }

        public async Task<Event?> DeleteEventById(int eventId)
        {
            var events = await FindEventById(eventId);

            if (events != null)
            {
                _context.Remove(events);
                await _context.SaveChangesAsync();
            }
            return events;
        }

        public async Task<Event?> FindEventById(int eventId)
        {
            return await _context.Events.FirstOrDefaultAsync();
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> UpdateEventById(int eventId, Event updateEvent)
        {
            Event events = await FindEventById(updateEvent.Id);
            if (events != null)
            {
                events.Title = updateEvent.Title;
                events.Description = updateEvent.Description;
                events.Price = updateEvent.Price;
                events.TimeofEvent = updateEvent.TimeofEvent;

                await _context.SaveChangesAsync();
                events = await FindEventById(events.Id);
            }
            return events;
        }
    }
}
