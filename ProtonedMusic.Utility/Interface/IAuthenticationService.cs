using ProtonedMusic.Utility.Models;

namespace ProtonedMusic.Utility.Interface
{
    
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateUser(string username, string password);
    }
}
