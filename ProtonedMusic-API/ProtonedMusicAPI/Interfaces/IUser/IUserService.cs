namespace ProtonedMusicAPI.Interfaces.IUser
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllAsync();
        Task<UserResponse?> FindByIdAsync(int userId);
        Task<UserResponse> CreateAsync(UserRequest newUser);
        Task<UserResponse?> UpdateByIdAsync(int userId, UserRequest updateUser);
        Task<UserResponse> DeleteByIdAsync(int userId);
        Task<LoginResponse> AuthenticateUser(LoginRequest login);
    }
}
