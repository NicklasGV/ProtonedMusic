using Microsoft.VisualBasic;
using System.Globalization;

namespace ProtonedMusicAPI.DTO.EventDTO
{
    public class UpcomingResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Timeof { get; set; }
    }
}
