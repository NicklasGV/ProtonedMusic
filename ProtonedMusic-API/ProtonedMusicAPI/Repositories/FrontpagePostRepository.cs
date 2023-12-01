using Microsoft.AspNetCore.Hosting.Server;
using ProtonedMusicAPI.Interfaces.IUser;
using static System.Net.WebRequestMethods;
using System.Net;
using System.Security.Cryptography.Xml;
using ProtonedMusicAPI.Interfaces.IFrontpage;
using Microsoft.IdentityModel.Tokens;

namespace ProtonedMusicAPI.Repositories
{
    public class FrontpagePostRepository : IFrontpagePostRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public FrontpagePostRepository(DatabaseContext databaseContext, IWebHostEnvironment hostingEnvironment)
        {
            _databaseContext = databaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<List<FrontpagePost>> GetAllAsync()
        {
            return await _databaseContext.Frontpages.ToListAsync();
        }

        public async Task<FrontpagePost> FindByIdAsync(int frontpageId)
        {
            return await _databaseContext.Frontpages.FirstOrDefaultAsync(f => f.Id == frontpageId);
        }

        public async Task<FrontpagePost> CreateAsync(FrontpagePost newFrontpage)
        {
            _databaseContext.Frontpages.Add(newFrontpage);
            await _databaseContext.SaveChangesAsync();
            return newFrontpage;
        }

        public async Task<FrontpagePost> DeleteByIdAsync(int frontpageId)
        {
            var frontpage = await FindByIdAsync(frontpageId);

            if (!string.IsNullOrEmpty(frontpage.FrontpagePicturePath))
            {
                if (!frontpage.FrontpagePicturePath.Contains("img"))
                {
                    await DeleteFileOnFtpAsync(frontpage.FrontpagePicturePath);
                }
            }

            if (frontpage != null)
            {
                _databaseContext.Remove(frontpage);
                await _databaseContext.SaveChangesAsync();
            }
            return frontpage;
        }

        public async Task<FrontpagePost> UpdateByIdAsync(int frontpageId, FrontpagePost updateFrontpage)
        {
            FrontpagePost frontpage = await FindByIdAsync(frontpageId);
            if (frontpage != null)
            {
                frontpage.Text = updateFrontpage.Text;
                frontpage.Banner = updateFrontpage.Banner;
                frontpage.FrontpagePicturePath = updateFrontpage.FrontpagePicturePath;

                await _databaseContext.SaveChangesAsync();

                frontpage = await FindByIdAsync(frontpage.Id);
            }
            return frontpage;
        }

        public async Task<FrontpagePost?> UploadFrontpagePicture(int frontpageId, IFormFile file)
        {
            string ftpUrl = "ftp://protonedmusic.com:EmanB65wrAdhcpekGH2F@nt7.unoeuro.com/public_html/assets/uploads/";

            FrontpagePost frontpage = await FindByIdAsync(frontpageId);
            string oldFilePath = frontpage.FrontpagePicturePath;

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
            frontpage.FrontpagePicturePath = Path.Combine("assets/uploads/", fileName);
            await UpdateByIdAsync(frontpageId, frontpage);

            return frontpage;
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
