using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProtonedMusicAPI.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProtonedMusicAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;
        private readonly IJwtUtils _jwtUtils;


        public UserService(IUserRepository userRepository, IConfiguration configuration, DatabaseContext context, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _context = context;
            _jwtUtils = jwtUtils;
        }

        public static UserResponse MapUserToUserResponse(User user)
        {
            UserResponse response = new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                AddressLineOne = user.AddressLineOne,
                AddressLineTwo = user.AddressLineTwo,
                Country = user.Country,
                City = user.City,
                Postal = user.Postal
            };
            return response;
        }

        private static User MapUserRequestToUser(UserRequest userRequest)
        {
            User user = new User
            {
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userRequest.Password) ?? string.Empty,
                Role = userRequest.Role,
                AddressLineOne = userRequest.AddressLineOne,
                AddressLineTwo = userRequest.AddressLineTwo,
                Country = userRequest.Country,
                City = userRequest.City,
                Postal = userRequest.Postal
            };
            return user;
        }

        public async Task<List<UserResponse>> GetAll()
        {
            List<User> users = await _userRepository.GetAll();

            if (users == null)
            {
                throw new ArgumentException();
            }
            return users.Select(user => MapUserToUserResponse(user)).ToList();
        }

        public async Task<UserResponse> FindById(int userId)
        {
            var user = await _userRepository.FindById(userId);

            if (user != null)
            {
                return MapUserToUserResponse(user);
            }

            return null;
        }

        public async Task<UserResponse> CreateUser(UserRequest newUser)
        {
            var user = await _userRepository.CreateUser(MapUserRequestToUser(newUser));
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            return MapUserToUserResponse(user);
        }

        public async Task<UserResponse> DeleteById(int userId)
        {
            var user = await _userRepository.DeleteById(userId);

            if (user != null)
            {
                return MapUserToUserResponse(user);
            }
            return null;
        }

        public async Task<UserResponse> UpdateUser(UserRequest updateUser)
        {
            var user = MapUserRequestToUser(updateUser);
            var insertedUser = await _userRepository.UpdateUser(user);

            if (insertedUser != null)
            {
                return MapUserToUserResponse(insertedUser);
            }

            return null;
        }

        public async Task<LoginModel> LoginUser(string email, string password)
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

        public async Task<LoginResponse> AuthenticateUser(LoginRequest login)
        {
            User user = await _userRepository.FindByEmail(login.Email);
            if (user == null)
            {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                LoginResponse response = new()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role,
                    Token = _jwtUtils.GenerateJwtToken(user)
                };
                return response;
            }
            return null;
        }
    }
}
