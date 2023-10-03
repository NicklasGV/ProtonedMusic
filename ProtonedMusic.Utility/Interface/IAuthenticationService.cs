using ProtonedMusic.Utility.Models;

namespace ProtonedMusic.Utility.Interface
{
    
    public interface IAuthenticationService
    {
        Task<LoginModel> AuthenticateUser(string username, string password);
    }
}
