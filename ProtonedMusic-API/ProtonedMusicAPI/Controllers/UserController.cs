namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        //private readonly IJwtUtils _jwtUtils;

        public UserController(IUserService userService/*, IJwtUtils jwtUtils*/, IUserRepository userRepository)
        {
            _userService = userService;
            //_jwtUtils = jwtUtils;
            _userRepository = userRepository;

        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("authenticate")]
        //public async Task<IActionResult> Authenticate([FromBody] LoginRequest login)
        //{
        //    try
        //    {
        //        LoginResponse user = await _userService.AuthenticateUser(login);
        //        if (user == null)
        //        {
        //            return Unauthorized();
        //        }

        //        return Ok(user);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}


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

        [HttpPut]
        [Route("{userId}")]
        public async Task<IActionResult> UpdateById([FromRoute] int userId, [FromBody] UserRequest updateUser)
        {
            try
            {
                var userResponse = await _userService.UpdateById(userId, updateUser);

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

        [HttpPost]
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
