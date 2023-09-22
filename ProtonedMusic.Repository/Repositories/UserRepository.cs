using ProtonedMusic.Repository.Database;
using ProtonedMusic.Utility.Models;

namespace ProtonedMusic.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        // DatabaseContext til dataadgang
        public DatabaseContext _context { get; set; }

        // Konstruktør, der tager en DatabaseContext som parameter
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        // Hent alle users fra databasen
        public async Task<List<UserModel>> GetAll()
        {
            return await _context.User.ToListAsync();
        }

        // Hent en user efter ID fra databasen
        public async Task<UserModel> FindById(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Id == userId);
        }

        // Hent en user efter email fra databasen
        public async Task<UserModel> FindByEmail(string email)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Email == email);
        }

        // Slet en user efter ID fra databasen
        public async Task<UserModel> DeleteById(int userId)
        {
            var userToDelete = await _context.User.FindAsync(userId);

            if (userToDelete != null)
            {
                _context.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
            return userToDelete;
        }

        // Opret en ny user i databasen
        public async Task<UserModel> CreateUser(UserModel newUser)
        {
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<UserModel> UpdateById(int userId, UserModel updateUser)
        {
            UserModel user = await FindById(userId);
            if (user != null)
            {
                user.FirstName = updateUser.FirstName;
                user.LastName = updateUser.LastName;
                user.Email = updateUser.Email;

                await _context.SaveChangesAsync();

                user = await FindById(user.Id);
            }
            return user;
        }
    }
}
