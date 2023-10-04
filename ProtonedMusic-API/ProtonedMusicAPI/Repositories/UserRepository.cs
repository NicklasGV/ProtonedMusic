namespace ProtonedMusicAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;
        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<User>> GetAll()
        {
            return await _databaseContext.Users
                .ToListAsync();
        }

        public async Task<User> FindById(int userId)
        {
            return await _databaseContext.Users.FirstOrDefaultAsync(s => s.Id == userId);
        }

        public async Task<User> FindByEmail(string email)
        {
            return await _databaseContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateUser(User newUser)
        {
            _databaseContext.Users.Add(newUser);
            await _databaseContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> DeleteById(int userId)
        {
            var user = await FindById(userId);

            if (user != null)
            {
                _databaseContext.Remove(user);
                await _databaseContext.SaveChangesAsync();
            }
            return user;
        }

        public async Task<User> UpdateById(int userId, User updateUser)
        {
            User user = await FindById(userId);
            if (user != null)
            {
                user.FirstName = updateUser.FirstName;
                user.LastName = updateUser.LastName;
                user.Email = updateUser.Email;
                user.Role = updateUser.Role;
                user.PhoneNumber = updateUser.PhoneNumber;
                user.AddressLineOne = updateUser.AddressLineOne;
                user.AddressLineTwo = updateUser.AddressLineTwo;
                user.City = updateUser.City;
                user.Postal = updateUser.Postal;
                user.Country = updateUser.Country;

                await _databaseContext.SaveChangesAsync();
                // incase the team was changed, get the hero and the correct team
                user = await FindById(user.Id);
            }
            return user;
        }
    }
}
