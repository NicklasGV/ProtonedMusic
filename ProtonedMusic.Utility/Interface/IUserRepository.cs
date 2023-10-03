namespace ProtonedMusic.Utility.Interface
{
    // Grænseflade til repositorylagets userrelaterede operationer
    public interface IUserRepository
    {
        // Metode til at hente alle usere
        public Task<List<UserModel>> GetAll();

        // Metode til at hente en user efter ID
        public Task<UserModel?> FindById(int userId);

        // Metode til at hente en user efter email
        public Task<UserModel> FindByEmail(string email);

        // Metode til at slette en email efter ID
        public Task<UserModel?> DeleteById(int userId);

        // Metode til at oprette en ny user
        public Task<UserModel> CreateUser(UserModel newUser);

        // Metode til at updatere en user
        public Task<UserModel?> UpdateUser(UserModel updateUser);
    }
}