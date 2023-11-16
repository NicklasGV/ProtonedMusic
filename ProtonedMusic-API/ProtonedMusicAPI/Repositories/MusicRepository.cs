using Microsoft.AspNetCore.Hosting.Server;
using ProtonedMusicAPI.Interfaces.IUser;
using static System.Net.WebRequestMethods;
using System.Net;
using System.Security.Cryptography.Xml;
using ProtonedMusicAPI.Interfaces.IFrontpage;

namespace ProtonedMusicAPI.Repositories
{
    public class MusicRepository : IMusicRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public MusicRepository(DatabaseContext databaseContext, IWebHostEnvironment hostingEnvironment)
        {
            _databaseContext = databaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<List<Music>> GetAllAsync()
        {
            return await _databaseContext.Music.ToListAsync();
        }

        public async Task<Music> FindByIdAsync(int musicId)
        {
            return await _databaseContext.Music.FirstOrDefaultAsync(m => m.Id == musicId);
        }

        public async Task<Music> CreateAsync(Music newMusic)
        {
            _databaseContext.Music.Add(newMusic);
            await _databaseContext.SaveChangesAsync();
            return newMusic;
        }

        public async Task<Music> DeleteByIdAsync(int musicId)
        {
            var music = await FindByIdAsync(musicId);

            if (music != null)
            {
                _databaseContext.Remove(music);
                await _databaseContext.SaveChangesAsync();
            }
            return music;
        }

        public async Task<Music> UpdateByIdAsync(int musicId, Music updateMusic)
        {
            Music music = await FindByIdAsync(musicId);
            if (music != null)
            {
                music.SongName = updateMusic.SongName;
                music.Artist = updateMusic.Artist;
                music.Album = updateMusic.Album;
                music.SongFilePath = updateMusic.SongFilePath;
                music.SongPicturePath = updateMusic.SongPicturePath;

                await _databaseContext.SaveChangesAsync();

                music = await FindByIdAsync(music.Id);
            }
            return music;
        }

        public async Task<Music?> UploadSong(int musicId, IFormFile songfile)
        {
            string ftpUrl = "ftp://protonedmusic.com:EmanB65wrAdhcpekGH2F@nt7.unoeuro.com/public_html/assets/music/";

            Music music = await FindByIdAsync(musicId);
            string oldFilePath = music.SongFilePath;

            if (!string.IsNullOrEmpty(oldFilePath))
            {
                if (!oldFilePath.Contains("img"))
                {
                    // If the user already has a profile picture, delete the old image asynchronously
                    await DeleteFileOnFtpAsync(oldFilePath);
                }
            }

            // Create an FTP request to upload the new profile picture
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(songfile.FileName);
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(new Uri(new Uri(ftpUrl), fileName));
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            using (var stream = songfile.OpenReadStream())
            using (var ftpStream = ftpRequest.GetRequestStream())
            {
                stream.CopyTo(ftpStream);
            }

            // Update the user's profile picture path in the database
            music.SongFilePath = Path.Combine("assets/music/", fileName);
            await UpdateByIdAsync(musicId, music);

            return music;
        }

        public async Task<Music?> UploadSongPicture(int musicId, IFormFile file)
        {
            string ftpUrl = "ftp://protonedmusic.com:EmanB65wrAdhcpekGH2F@nt7.unoeuro.com/public_html/assets/uploads/";

            Music music = await FindByIdAsync(musicId);
            string oldFilePath = music.SongPicturePath;

            if (!string.IsNullOrEmpty(oldFilePath))
            {
                if (!oldFilePath.Contains("img"))
                {
                    // If the user already has a profile picture, delete the old image asynchronously
                    await DeleteFileOnFtpAsync(oldFilePath);
                }
            }

            // Create an FTP request to upload the new profile picture
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(new Uri(new Uri(ftpUrl), fileName));
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            using (var stream = file.OpenReadStream())
            using (var ftpStream = ftpRequest.GetRequestStream())
            {
                stream.CopyTo(ftpStream);
            }

            // Update the user's profile picture path in the database
            music.SongPicturePath = Path.Combine("assets/uploads/", fileName);
            await UpdateByIdAsync(musicId, music);

            return music;
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
