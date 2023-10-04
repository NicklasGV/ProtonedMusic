namespace ProtonedMusicAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        //private readonly IJwtUtils _jwtUtils;


        public UserService(IUserRepository userRepository/*, IJwtUtils jwtUtils*/)
        {
            _userRepository = userRepository;
            //_jwtUtils = jwtUtils;
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
                Password = userRequest.Password ?? string.Empty,
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

        public async Task<UserResponse> UpdateById(int userId, UserRequest updateUser)
        {
            var user = MapUserRequestToUser(updateUser);
            var insertedUser = await _userRepository.UpdateById(userId, user);

            if (insertedUser != null)
            {
                return MapUserToUserResponse(insertedUser);
            }

            return null;
        }

        //public async Task<LoginResponse> AuthenticateUser(LoginRequest login)
        //{
        //    User user = await _userRepository.FindByEmail(login.Email);
        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    if (user.Password == login.Password)
        //    {
        //        LoginResponse response = new()
        //        {
        //            Id = user.Id,
        //            Email = user.Email,
        //            Role = user.Role,
        //            Token = _jwtUtils.GenerateJwtToken(user)
        //        };
        //        return response;
        //    }
        //    return null;
        //}
    }
}
