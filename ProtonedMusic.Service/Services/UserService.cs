using Microsoft.IdentityModel.Tokens;
using System.Net.Sockets;

namespace ProtonedMusic.Service.Services
{
    public class UserService : IUserService
    {
        // Repository til dataadgang
        public IUserRepository _userRepository { get; set; }

        // Konstruktør, der tager et IUserRepository som parameter
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

    }
}

