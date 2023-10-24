namespace ProtonedMusicAPI.Interfaces.IUser
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAll();
        Task<UserResponse?> FindById(int userId);
        Task<UserResponse> CreateUser(UserRequest newUser);
        Task<UserResponse?> UpdateUser(int userId, UserRequest updateUser);
        Task<UserResponse> DeleteById(int userId);
        Task<LoginResponse> AuthenticateUser(LoginRequest login);
    }
}
