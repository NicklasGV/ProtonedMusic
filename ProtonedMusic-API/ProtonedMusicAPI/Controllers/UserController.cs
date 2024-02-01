using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Hosting.Internal;
using SendGrid.Helpers.Errors.Model;
using Stripe.Tax;

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
        public async Task<IActionResult> UpdateByIdAsync([FromRoute] int userId, [FromForm] UserRequest updateUser)
        {
            try
            {
                var userResponse = await _userService.UpdateByIdAsync(userId, updateUser);

                if (updateUser.PictureFile != null)
                {
                    UserResponse userPicture = await _userService.UploadProfilePicture(userResponse.Id, updateUser.PictureFile);

                    if (userPicture != null)
                    {
                        userResponse = userPicture;
                    }

                }

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

        //[Authorize(Role.Admin, Role.Customer)]
        [HttpPost]
        [Route("Update/{userId}")]
        public async Task<IActionResult> UpdateByIdNoPassword([FromRoute] int userId, [FromForm] UserRequestNoPassword updateUser)
        {
            try
            {
                var userResponse = await _userService.UpdateByIdNoPassword(userId, updateUser);

                if (updateUser.PictureFile != null)
                {
                    UserResponse userPicture = await _userService.UploadProfilePicture(userResponse.Id, updateUser.PictureFile);

                    if (userPicture != null)
                    {
                        userResponse = userPicture;
                    }

                }

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
        public async Task<IActionResult> CreateAsync([FromForm] UserRequest newUser)
        {
            try
            {
                var mail = await _userService.FindByEmailAsync(newUser.Email);
                if (mail != null)
                {
                    return Conflict("Email is already in use");
                }

                UserResponse userResponse = await _userService.CreateAsync(newUser);

                if (newUser.PictureFile != null)
                {
                    UserResponse userPicture = await _userService.UploadProfilePicture(userResponse.Id, newUser.PictureFile);

                    if (userPicture != null)
                    {
                        userResponse = userPicture;
                    }

                }

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

        [HttpDelete]
        [Route("remove-picture/{userId}")]
        public async Task<IActionResult> RemoveProfilePicture([FromRoute] int userId)
        {
            try
            {
                UserResponse user = await _userService.RemoveProfilePicture(userId);

                if (user != null)
                {
                    return Ok(user.ProfilePicturePath);
                }
                return Problem();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnnonymous]
        [HttpPost]
        [Route("Newsletter/Subscribe/{email}")]
        public async Task<IActionResult> SubscribeNewsletter([FromRoute] string email)
        {
            try
            {
                AddonRoles newsletter = (AddonRoles)1;
                UserResponse user = await _userService.SubscribeNewsletter(email, newsletter);

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

        [AllowAnnonymous]
        [HttpPost]
        [Route("Newsletter/Unsubscribe/{email}")]
        public async Task<IActionResult> UnsubscribeNewsletter([FromRoute] string email)
        {
            try
            {
                AddonRoles newsletter = 0;
                UserResponse user = await _userService.SubscribeNewsletter(email, newsletter);

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

        [HttpGet]
        [Route("Email/{email}")]
        public async Task<IActionResult> FindByEmailAsync([FromRoute] string email)
        {
            try
            {
                UserResponse userResponse = await _userService.FindByEmailAsync(email);

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
    }
}
