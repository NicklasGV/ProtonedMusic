using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Hosting.Internal;

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

        [Authorize(Role.Admin, Role.Customer)]
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> FindByIdAsync([FromRoute] int userId)
        {
            try
            {
                UserResponse userResponse = await _userService.FindByIdAsync(userId);

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
        public async Task<IActionResult> UpdateByIdAsync([FromRoute] int userId, [FromBody] UserRequest updateUser)
        {
            try
            {
                var userResponse = await _userService.UpdateByIdAsync(userId, updateUser);

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
        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] int userId)
        {
            try
            {
                var userResponse = await _userService.DeleteByIdAsync(userId);
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
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<UserResponse> users = await _userService.GetAllAsync();

                if (users == null)
                {
                    return Problem("A problem occured the team is fixing it as we speak");
                }

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
        public async Task<IActionResult> CreateAsync([FromBody] UserRequest newUser)
        {
            try
            {
                UserResponse userResponse = await _userService.CreateAsync(newUser);
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("upload-profile-picture/{userId}")]
        public async Task<IActionResult> UploadProfilePicture([FromRoute] int userId, IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            if (file != null)
            {
                UserResponse user = await _userService.UploadProfilePicture(userId, file);

                if (user != null)
                {
                    return Ok(user.ProfilePicturePath);
                }
                
            }

            return BadRequest("No file was uploaded.");
        }

        [HttpPost]
        [Route("Newsletter/{userId}")]
        public async Task<IActionResult> SubscribeNewsletter([FromRoute] int userId, [FromBody] AddonRoles newsletter)
        {
            try
            {
                UserResponse user = await _userService.SubscribeNewsletter(userId, newsletter);

                if (user != null)
                {
                    return Ok(user.AddonRoles);
                }
                return Problem();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
