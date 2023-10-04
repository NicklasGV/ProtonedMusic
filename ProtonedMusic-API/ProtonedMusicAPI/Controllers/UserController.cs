using AuthorizeAttribute = ProtonedMusicAPI.Authentication.AuthorizeAttribute;
namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UserController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;

        }

        [AllowAnnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] LoginRequest login)
        {
            try
            {
                LoginResponse user = await _userService.AuthenticateUser(login);
                if (user == null)
                {
                    return Unauthorized();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var auth = await _userService.LoginUser(email, password);

            if (auth is null)
            {
                return Unauthorized("Bad login");
            }

            return Ok(auth);
        }

        [Authorize(Role.Admin, Role.Customer)]
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> FindById([FromRoute] int userId)
        {
            try
            {
                UserResponse userResponse = await _userService.FindById(userId);

                if (userResponse == null)
                {
                    return NotFound();
                }
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.Admin, Role.Customer)]
        [HttpPut]
        [Route("{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserRequest updateUser)
        {
            try
            {
                var userResponse = await _userService.UpdateUser(updateUser);

                if (userResponse == null)
                {
                    return NotFound();
                }

                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> DeleteById([FromRoute] int userId)
        {
            try
            {
                var userResponse = await _userService.DeleteById(userId);
                if (userResponse == null)
                {
                    return NotFound();
                }
                return Ok(userResponse);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<UserResponse> users = await _userService.GetAll();

                if (users.Count == 0)
                {
                    return NoContent();
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [AllowAnnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest newUser)
        {
            try
            {
                UserResponse userResponse = await _userService.CreateUser(newUser);
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
