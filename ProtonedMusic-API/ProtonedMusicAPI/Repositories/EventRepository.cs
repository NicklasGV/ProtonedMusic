using Microsoft.EntityFrameworkCore.Diagnostics;
using ProtonedMusicAPI.Database.Entities;
using ProtonedMusicAPI.Database;
using System.Net;

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
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == eventId);
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> UpdateEventById(int eventId, Event updateEvent)
        {
            Event events = await FindEventById(eventId);
            if (events != null)
            {
                events.Title = updateEvent.Title;
                events.Description = updateEvent.Description;
                events.Price = updateEvent.Price;
                events.TimeofEvent = updateEvent.TimeofEvent;

                await _context.SaveChangesAsync();
                events = await FindEventById(eventId);
            }
            return events;
        }

        public async Task<Event?> UploadEventPicture(int eventId, IFormFile file)
        {
            string ftpUrl = "ftp://protonedmusic.com:EmanB65wrAdhcpekGH2F@nt7.unoeuro.com/public_html/assets/uploads/";

            Event events = await FindEventById(eventId);
            string oldFilePath = events.EventPicturePath;

            if (!string.IsNullOrEmpty(oldFilePath))
            {
                // If the event already has a event picture, delete the old image asynchronously
                await DeleteFileOnFtpAsync(oldFilePath);
            }

            // Create an FTP request to upload the new event picture
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(new Uri(new Uri(ftpUrl), fileName));
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            using (var stream = file.OpenReadStream())
            using (var ftpStream = ftpRequest.GetRequestStream())
            {
                stream.CopyTo(ftpStream);
            }

            // Update the event's event picture path in the database
            events.EventPicturePath = Path.Combine("assets/uploads/", fileName);
            await UpdateEventById(eventId, events);

            return events;
        }

        public async Task DeleteFileOnFtpAsync(string filePath)
        {
            string ftpUrl = "ftp://protonedmusic.com:EmanB65wrAdhcpekGH2F@nt7.unoeuro.com/public_html/";
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(new Uri(new Uri(ftpUrl), filePath));
            ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;

            try
            {
                FtpWebResponse ftpResponse = (FtpWebResponse)await ftpRequest.GetResponseAsync();
                Console.WriteLine($"File deleted, status: {ftpResponse.StatusDescription}");
                ftpResponse.Close();
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Error deleting file: {ex.Message}");
            }
        }
    }
}
