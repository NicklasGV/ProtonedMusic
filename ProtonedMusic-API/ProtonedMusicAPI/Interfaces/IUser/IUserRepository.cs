namespace ProtonedMusicAPI.Interfaces.IUser
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> FindByIdAsync(int userId);
        Task<User> FindByEmail(string email);
        Task<User> CreateAsync(User newUser);
        Task<User?> UpdateByIdAsync(int userId, User updateUser);
        Task<User?> DeleteByIdAsync(int userId);
        Task<User?> UploadProfilePicture(int userId, IFormFile file);
        Task<User?> SubscribeNewsletter(int userId, AddonRoles updateNewsletter);
    }
}
