namespace ProtonedMusicAPI.Interfaces.IUser
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAll();
        Task<UserResponse?> FindById(int userId);
        Task<UserResponse> CreateUser(UserRequest newUser);
        Task<UserResponse?> UpdateById(int userId, UserRequest updateUser);
        Task<UserResponse> DeleteById(int userId);

        //Login
        //Task<LoginResponse> AuthenticateUser(LoginRequest login);
    }
}
