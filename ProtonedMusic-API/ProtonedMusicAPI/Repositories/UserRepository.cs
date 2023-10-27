namespace ProtonedMusicAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;
        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _databaseContext.User
                .Include(u => u.NewsLikes)
                .ThenInclude(nl => nl.News)
                .ToListAsync();
        }

        public async Task<User> FindByIdAsync(int userId)
        {
            return await _databaseContext.User
                .Include(u => u.NewsLikes)
                .ThenInclude(nl => nl.News)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> FindByEmail(string email)
        {
            return await _databaseContext.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User newUser)
        {
            _databaseContext.User.Add(newUser);
            await _databaseContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> DeleteByIdAsync(int userId)
        {
            var user = await FindByIdAsync(userId);

            if (user != null)
            {
                _databaseContext.Remove(user);
                await _databaseContext.SaveChangesAsync();
            }
            return user;
        }

        public async Task<User> UpdateByIdAsync(int userId, User updateUser)
        {
            User user = await FindByIdAsync(userId);
            if (user != null)
            {
                user.FirstName = updateUser.FirstName;
                user.LastName = updateUser.LastName;
                user.Email = updateUser.Email;
                user.Role = updateUser.Role;
                user.PhoneNumber = updateUser.PhoneNumber;
                user.Address = updateUser.Address;
                user.City = updateUser.City;
                user.Postal = updateUser.Postal;
                user.Country = updateUser.Country;

                await _databaseContext.SaveChangesAsync();

                user = await FindByIdAsync(user.Id);
            }
            return user;
        }
    }
}
