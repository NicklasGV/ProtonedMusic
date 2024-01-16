namespace ProtonedMusicAPI.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ArtistRepository(DatabaseContext databaseContext, IWebHostEnvironment hostingEnvironment)
        {
            _databaseContext = databaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<List<Artist>> GetAllAsync()
        {
            return await _databaseContext.Artist
                .Include(a => a.Songs)
                .ThenInclude(s => s.Music)
                .Include(a => a.Links)
                .Include(a => a.User)
                .ToListAsync();
        }

        public async Task<Artist> FindByIdAsync(int artistId)
        {
            return await _databaseContext.Artist
                .Include(a => a.Songs)
                .ThenInclude(s => s.Music)
                .Include(a => a.Links)
                .Include(a => a.User)
                .FirstOrDefaultAsync(u => u.Id == artistId);
        }

        public async Task<Artist> CreateAsync(Artist newArtist)
        {
            _databaseContext.Artist.Add(newArtist);
            await _databaseContext.SaveChangesAsync();
            newArtist = await FindByIdAsync(newArtist.Id);
            return newArtist;
        }

        public async Task<Artist> DeleteByIdAsync(int artistId)
        {
            var artist = await FindByIdAsync(artistId);

            if (!string.IsNullOrEmpty(artist.PicturePath))
            {
                await DeleteFileOnFtpAsync(artist.PicturePath);
            }

            if (artist != null)
            {
                _databaseContext.Remove(artist);
                await _databaseContext.SaveChangesAsync();
            }
            return artist;
        }

        public async Task<Artist> UpdateByIdAsync(int artistId, Artist updateArtist)
        {
            Artist artist = await FindByIdAsync(artistId);
            if (artist != null)
            {
                artist.Name = updateArtist.Name;
                artist.Info = updateArtist.Info;
                artist.PicturePath = updateArtist.PicturePath;
                
                artist.UserId = updateArtist.UserId;
                artist.Songs = updateArtist.Songs;
                artist.Links = updateArtist.Links;

                await _databaseContext.SaveChangesAsync();

                artist = await FindByIdAsync(artistId);
            }
            return artist;
        }

        public async Task<Artist?> UploadPicture(int artistId, IFormFile file)
        {
            string ftpUrl = "ftp://protonedmusic.com:EmanB65wrAdhcpekGH2F@nt7.unoeuro.com/public_html/assets/uploads/";

            Artist artist = await FindByIdAsync(artistId);
            string oldFilePath = artist.PicturePath;

            if (!string.IsNullOrEmpty(oldFilePath))
            {
                await DeleteFileOnFtpAsync(oldFilePath);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(new Uri(new Uri(ftpUrl), fileName));
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            using (var stream = file.OpenReadStream())
            using (var ftpStream = ftpRequest.GetRequestStream())
            {
                stream.CopyTo(ftpStream);
            }

            artist.PicturePath = Path.Combine("assets/uploads/", fileName);
            await UpdateByIdAsync(artistId, artist);

            return artist;
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
