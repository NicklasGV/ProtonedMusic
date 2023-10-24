﻿namespace ProtonedMusicAPI.DTO.EventDTO
{
    public class EventResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal price { get; set; }
        public DateTime TimeofEvent { get; set; }
        public DateTime Created { get; set; }
    }
}
