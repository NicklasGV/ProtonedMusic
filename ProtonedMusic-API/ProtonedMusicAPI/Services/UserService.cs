using ProtonedMusicAPI.DTO.EmailDTO;

namespace ProtonedMusicAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;
        private readonly IEmailService _emailService;


        public UserService(IUserRepository userRepository, IJwtUtils jwtUtils, IEmailService emailService)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
            _emailService = emailService;
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

        public static UserResponse MapUserToUserResponse(User user)
        {
            UserResponse response = new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email.ToLower(),
                Role = user.Role,
                AddonRoles = user.AddonRoles,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Country = user.Country,
                City = user.City,
                Postal = user.Postal,
                ProfilePicturePath = user.ProfilePicturePath,

            };
            if (user.NewsLikes.Count > 0)
            {
                response.NewsLikes = user.NewsLikes.Select(x => new UserNewsLikeResponse
                {
                    Id = x.News.Id,
                    Title = x.News.Title,
                    Text = x.News.Text,
                    DateTime = x.News.DateTime,
                }).ToList();
            }
            return response;
        }

        private static User MapUserRequestToUser(UserRequest userRequest)
        {
            User user = new User
            {
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email.ToLower(),
                Password = BCrypt.Net.BCrypt.HashPassword(userRequest.Password) ?? string.Empty,
                Role = userRequest.Role,
                AddonRoles = userRequest.AddonRoles,
                PhoneNumber = userRequest.PhoneNumber,
                Address = userRequest.Address,
                Country = userRequest.Country,
                City = userRequest.City,
                Postal = userRequest.Postal,
                ProfilePicturePath = userRequest.ProfilePicturePath ?? string.Empty,
            };
            return user;
        }

        public async Task<List<UserResponse>> GetAllAsync()
        {
            List<User> users = await _userRepository.GetAllAsync();

            if (users == null)
            {
                throw new ArgumentException();
            }
            return users.Select(MapUserToUserResponse).ToList();
        }

        public async Task<UserResponse> FindByIdAsync(int userId)
        {
            var user = await _userRepository.FindByIdAsync(userId);

            if (user != null)
            {
                return MapUserToUserResponse(user);
            }

            return null;
        }

        public async Task<UserResponse> CreateAsync(UserRequest newUser)
        {
            var user = await _userRepository.CreateAsync(MapUserRequestToUser(newUser));
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            return MapUserToUserResponse(user);
        }

        public async Task<UserResponse> DeleteByIdAsync(int userId)
        {
            var user = await _userRepository.DeleteByIdAsync(userId);

            if (user != null)
            {
                return MapUserToUserResponse(user);
            }
            return null;
        }

        public async Task<UserResponse> UpdateByIdAsync(int userId, UserRequest updateUser)
        {
            var user = MapUserRequestToUser(updateUser);
            var insertedUser = await _userRepository.UpdateByIdAsync(userId, user);

            if (insertedUser != null)
            {
                return MapUserToUserResponse(insertedUser);
            }

            return null;
        }

        public async Task<UserResponse> UploadProfilePicture(int userId, IFormFile file)
        {
            User user = await _userRepository.UploadProfilePicture(userId, file);

            if (user != null)
            {
                return MapUserToUserResponse(user);
            }

            return null;

        }

        public async Task<UserResponse> SubscribeNewsletter(string email, AddonRoles updateNewsletter)
        {
            User user = await _userRepository.SubscribeNewsletter(email, updateNewsletter);

            if (user != null)
            {
                if (user.AddonRoles == (AddonRoles)1)
                {
                    EmailResponse mailData = new EmailResponse();
                    mailData.To = user.Email;
                    mailData.Subject = "Welcome to Protoned Music";
                    mailData.Body = "Welcome " + user.FirstName + "<br> <br>" + "We'll be sending you a newsletter every month containing new shows, merchandise and more!" + "<br><br>" +
                        "We can't wait for you to join us in this journey, and we are beyond excited to have you join this newsletter.";
                    _emailService.SendEMail(mailData);
                }
                return MapUserToUserResponse(user);
            }
            return null;
        }

        public async Task<UserResponse> FindByEmailAsync(string email)
        {
            var user = await _userRepository.FindByEmail(email);

            if (user != null)
            {
                return MapUserToUserResponse(user);
            }

            return null;
        }
    }
}
