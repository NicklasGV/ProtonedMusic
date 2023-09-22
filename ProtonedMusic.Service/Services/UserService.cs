using Microsoft.IdentityModel.Tokens;
using System.Net.Sockets;

namespace ProtonedMusic.Service.Services
{
    public class UserService : IUserService
    {
        // Repository til dataadgang
        public IUserRepository _userRepository { get; set; }

        // Konstruktør, der tager et IProductRepository som parameter
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Metode til at hente alle produkter
        public async Task<List<UserModel>> GetAll()
        {
            // Kalder GetAllProduct-metoden i det underliggende repository for at hente produkter
            return await _userRepository.GetAll();
        }

        // Metode til at hente et produkt efter ID
        public async Task<UserModel> FindById(int userId)
        {
            // Kalder GetProductById-metoden i det underliggende repository for at hente et produkt efter ID
            return await _userRepository.FindById(userId);
        }

        // Metode til at slette et produkt efter ID
        public async Task<UserModel> DeleteById(int userId)
        {
            // Kalder DeleteProductById-metoden i det underliggende repository for at slette et produkt efter ID
            return await _userRepository.DeleteById(userId);
        }

        // Metode til at oprette et nyt produkt
        public async Task<UserModel> CreateUser(UserModel newUser)
        {
            // Kalder CreateProduct-metoden i det underliggende repository for at oprette et nyt produkt
            return await _userRepository.CreateUser(newUser);
        }

        public async Task<UserModel> UpdateById(int userId, UserModel updateUser)
        {
            var user = MapUserRequestToUser(updateUser);
            var insertedUser = await _userRepository.UpdateById(userId, user);

            if (insertedUser != null)
            {
                return MapUserToUserResponse(insertedUser);
            }

            return null;
        }

        public static UserModel MapUserToUserResponse(UserModel user)
        {
            UserModel response = new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                City = user.City,
                Postal = user.Postal,
                Country = user.Country,
                
            };
            return response;
        }

        private static UserModel MapUserRequestToUser(UserModel userRequest)
        {
            UserModel user = new UserModel
            {
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
                PhoneNumber = userRequest.PhoneNumber,
                Address = userRequest.Address,
                City = userRequest.City,
                Postal = userRequest.Postal,
                Country = userRequest.Country,
            };
            return user;
        }
    }
}
