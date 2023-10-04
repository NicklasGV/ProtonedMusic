namespace ProtonedMusicAPI.Interfaces.IUser
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User?> FindById(int userId);
        Task<User> FindByEmail(string email);
        Task<User> CreateUser(User newUser);
        Task<User?> UpdateById(int userId, User updateUser);
        Task<User?> DeleteById(int userId);
    }
}
