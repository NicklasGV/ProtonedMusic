using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProtonedMusic.Repository.Database;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProtonedMusic.Service.Services
{
    public class UserService : IUserService
    {
        // Repository til dataadgang
        public IUserRepository _userRepository { get; set; }
        public IConfiguration _configuration { get; set; }
        public DatabaseContext _context { get; set; }

        // Konstruktør, der tager et IUserRepository som parameter
        public UserService(IUserRepository userRepository, IConfiguration configuration, DatabaseContext context)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _context = context;
        }

        // Metode til at hente alle users
        public async Task<List<UserModel>> GetAll()
        {
            // Kalder GetAllUser-metoden i det underliggende repository for at hente users
            return await _userRepository.GetAll();
        }

        // Metode til at hente en user efter ID
        public async Task<UserModel> FindById(int userId)
        {
            // Kalder GetUserById-metoden i det underliggende repository for at hente en user efter ID
            return await _userRepository.FindById(userId);
        }

        // Metode til at slette en user efter ID
        public async Task<UserModel> DeleteById(int userId)
        {
            // Kalder DeleteUserById-metoden i det underliggende repository for at slette en user efter ID
            return await _userRepository.DeleteById(userId);
        }

        // Metode til at oprette en ny user
        public async Task<UserModel> CreateUser(UserModel newUser)
        {
            // Hash the password before storing it
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            // Set the hashed password in the user object
            newUser.Password = hashedPassword;

            // Call the repository to create the user
            return await _userRepository.CreateUser(newUser);
        }

        public async Task<UserModel> UpdateUser(UserModel updateUser)
        {
            var user = await _userRepository.FindById(updateUser.Id);

            if (user is null)
            {
                return null;
            }

            user.FirstName = updateUser.FirstName;
            user.LastName = updateUser.LastName;
            user.Email = updateUser.Email;
            user.PhoneNumber = updateUser.PhoneNumber;
            user.Address = updateUser.Address;
            user.City = updateUser.City;
            user.Postal = updateUser.Postal;
            user.Country = updateUser.Country;

            // Check if the password is being updated and hash it if necessary
            if (!string.IsNullOrWhiteSpace(updateUser.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(updateUser.Password);
            }

            // Update other user properties as needed

            return await _userRepository.UpdateUser(user);
        }

        public async Task<LoginModel> AuthenticateUser(string email, string password)
        {
            var user = await _userRepository.FindByEmail(email);

            if (user == null)
            {
                return null; // User not found
            }

            // Use BCrypt to verify the hashed password
            bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                string newAccessToken = CreateJwtToken();
                string newRefreshToken = CreateRefreshToken();
                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);
                await _userRepository.UpdateUser(user);

                LoginModel model = new LoginModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role,
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                };
                return model;
            }
            return null;
        }

        public string CreateJwtToken()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signInCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }

        public string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            // Check if token exists in the Database already.
            var tokenInUser = _context.User.Any(u => u.RefreshToken == refreshToken);
            if (tokenInUser)
            {
                // If token already exists then run the method again.
                return CreateRefreshToken();
            }
            return refreshToken;
        }

    }
}

