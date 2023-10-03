

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProtonedMusic.Repository.Database;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class AuthenticationService : IAuthenticationService
{
    public IUserRepository _userRepository { get; set; }
    public IConfiguration _configuration { get; set; }
    public DatabaseContext _context { get; set; }

    public AuthenticationService(IUserRepository userRepository, IConfiguration configuration, DatabaseContext context)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _context = context;
    }

    public async Task<LoginModel> AuthenticateUser(string email, string password)
    {
        var user = await _userRepository.FindByEmail(email);

        if (user == null)
        {
            return null; // User not found
        }

        // Use BCrypt to verify the hashed password
        bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            string newAccessToken = CreateJwtToken();
            string newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);
            await _userRepository.UpdateUser(user);

            LoginModel model = new LoginModel()
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };
            return model;
        }
        return null;
    }

    public string CreateJwtToken()
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
        var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            claims: new List<Claim>(),
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: signInCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
    }

    public string CreateRefreshToken()
    {
        var tokenBytes = RandomNumberGenerator.GetBytes(64);
        var refreshToken = Convert.ToBase64String(tokenBytes);

        // Check if token exists in the Database already.
        var tokenInUser = _context.User.Any(u => u.RefreshToken == refreshToken);
        if (tokenInUser)
        {
            // If token already exists then run the method again.
            return CreateRefreshToken();
        }
        return refreshToken;
    }
}

