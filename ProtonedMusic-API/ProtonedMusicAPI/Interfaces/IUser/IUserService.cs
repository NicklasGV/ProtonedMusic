namespace ProtonedMusicAPI.Interfaces.IUser
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAll();
        Task<UserResponse?> FindById(int userId);
        Task<UserResponse> CreateUser(UserRequest newUser);
        Task<UserResponse?> UpdateUser(UserRequest updateUser);
        Task<UserResponse> DeleteById(int userId);

        public Task<LoginModel> AuthenticateUser(string username, string password);
    }
}
