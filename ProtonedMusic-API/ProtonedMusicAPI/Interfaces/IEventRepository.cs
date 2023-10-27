﻿namespace ProtonedMusicAPI.Interfaces
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();
        Task<Event> CreateEvent(Event newEvent);
        Task<Event?> FindEventById(int eventId);
        Task<Event?> UpdateEventById(int eventId, Event updateEvent);
        Task<Event?> DeleteEventById(int eventId);
    }
}
