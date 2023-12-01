namespace ProtonedMusicAPI.Interfaces.IUser
{
    public interface IUserService
    {
        Task<LoginResponse> AuthenticateUser(LoginRequest login);
        Task<List<UserResponse>> GetAllAsync();
        Task<UserResponse?> FindByIdAsync(int userId);
        Task<UserResponse> CreateAsync(UserRequest newUser);
        Task<UserResponse?> UpdateByIdAsync(int userId, UserRequest updateUser);
        Task<UserResponse> DeleteByIdAsync(int userId);
        Task<UserResponse> UploadProfilePicture(int userId, IFormFile file);
        Task<UserResponse> SubscribeNewsletter(string email, AddonRoles updateNewsletter);
        Task<UserResponse> FindByEmailAsync(string email);
    }
}
