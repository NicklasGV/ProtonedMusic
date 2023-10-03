namespace ProtonedMusic.Utility.Interface
{
    // Grænseflade til tjenestelagets userrelaterede operationer
    public interface IUserService
    {
        // Metode til at hente alle usere
        public Task<List<UserModel>> GetAll();

        // Metode til at hente en user efter ID
        public Task<UserModel?> FindById(int userId);

        // Metode til at slette en user efter ID
        public Task<UserModel> DeleteById(int userId);

        // Metode til at oprette en nyn user
        public Task<UserModel> CreateUser(UserModel newUser);

        // Metode til at updatere en user
        public Task<UserModel?> UpdateUser(UserModel updateUser);
    }
}
