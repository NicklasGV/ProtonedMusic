

public class AuthenticationService : IAuthenticationService
{
    public IUserRepository _userRepository { get; set; }

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> AuthenticateUser(string email, string password)
    {
        var user = await _userRepository.FindByEmail(email);

        if (user == null)
        {
            return false; // User not found
        }

        // Use BCrypt to verify the hashed password
        bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, user.Password);

        return passwordMatch;
    }

    // Other authentication-related methods can go here, such as token generation.
}

