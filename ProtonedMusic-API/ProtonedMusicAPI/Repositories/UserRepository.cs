using Microsoft.AspNetCore.Hosting.Server;
using ProtonedMusicAPI.Interfaces.IUser;
using static System.Net.WebRequestMethods;
using System.Net;
using System.Security.Cryptography.Xml;

namespace ProtonedMusicAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UserRepository(DatabaseContext databaseContext, IWebHostEnvironment hostingEnvironment)
        {
            _databaseContext = databaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _databaseContext.User
                .Include(u => u.NewsLikes)
                .ThenInclude(nl => nl.News)
                .ToListAsync();
        }

        public async Task<User> FindByIdAsync(int userId)
        {
            return await _databaseContext.User
                .Include(u => u.NewsLikes)
                .ThenInclude(nl => nl.News)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> FindByEmail(string email)
        {
            return await _databaseContext.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User newUser)
        {
            _databaseContext.User.Add(newUser);
            await _databaseContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> DeleteByIdAsync(int userId)
        {
            var user = await FindByIdAsync(userId);

            if (!string.IsNullOrEmpty(user.ProfilePicturePath))
            {
                await DeleteFileOnFtpAsync(user.ProfilePicturePath);
            }

            if (user != null)
            {
                _databaseContext.Remove(user);
                await _databaseContext.SaveChangesAsync();
            }
            return user;
        }

        public async Task<User> UpdateByIdAsync(int userId, User updateUser)
        {
            User user = await FindByIdAsync(userId);
            if (user != null)
            {
                user.FirstName = updateUser.FirstName;
                user.LastName = updateUser.LastName;
                user.Email = updateUser.Email;
                user.Password = updateUser.Password;
                user.Role = updateUser.Role;
                user.AddonRoles = updateUser.AddonRoles;
                user.PhoneNumber = updateUser.PhoneNumber;
                user.Address = updateUser.Address;
                user.City = updateUser.City;
                user.Postal = updateUser.Postal;
                user.Country = updateUser.Country;
                user.ProfilePicturePath = updateUser.ProfilePicturePath;

                await _databaseContext.SaveChangesAsync();

                user = await FindByIdAsync(user.Id);
            }
            return user;
        }

        public async Task<User> UpdateByIdNoPassword(int userId, User updateUser)
        {
            User user = await FindByIdAsync(userId);
            if (user != null)
            {
                user.FirstName = updateUser.FirstName;
                user.LastName = updateUser.LastName;
                user.Email = updateUser.Email;
                user.Role = updateUser.Role;
                user.AddonRoles = updateUser.AddonRoles;
                user.PhoneNumber = updateUser.PhoneNumber;
                user.Address = updateUser.Address;
                user.City = updateUser.City;
                user.Postal = updateUser.Postal;
                user.Country = updateUser.Country;
                user.ProfilePicturePath = updateUser.ProfilePicturePath;

                await _databaseContext.SaveChangesAsync();

                user = await FindByIdAsync(user.Id);
            }
            return user;
        }

        public async Task<User?> UploadProfilePicture(int userId, IFormFile file)
        {
            string ftpUrl = "ftp://protonedmusic.com:EmanB65wrAdhcpekGH2F@nt7.unoeuro.com/public_html/assets/uploads/";

            User user = await FindByIdAsync(userId);
            string oldFilePath = user.ProfilePicturePath;

            if (!string.IsNullOrEmpty(oldFilePath))
            {
                // If the user already has a profile picture, delete the old image asynchronously
                await DeleteFileOnFtpAsync(oldFilePath);
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
            user.ProfilePicturePath = Path.Combine("assets/uploads/", fileName);
            await UpdateByIdAsync(userId, user);

            return user;
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

        public async Task<User?> SubscribeNewsletter(string email, AddonRoles updateNewsletter)
        {
            User user = await FindByEmail(email);

            if (user != null)
            {
                user.AddonRoles = updateNewsletter;

                await _databaseContext.SaveChangesAsync();

                user = await FindByIdAsync(user.Id);
            }
            return user;
        }
    }
}
