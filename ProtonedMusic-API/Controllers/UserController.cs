using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Environ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService { get; set; }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Hent alle users fra UserService
            return Ok(await _userService.GetAll());
        }

        [HttpDelete("userId")]
        public async Task<IActionResult> DeleteById([FromRoute] int userId)
        {
            // Hent useren med det angivne ID fra UserService
            var user = await _userService.FindById(userId);

            // Hvis useren ikke findes, returner en 404 Not Found-response
            if (user == null)
            {
                return NotFound(); // Returnerer 404 hvis useren ikke findes..
            }

            // Slet useren med det angivne ID fra UserService
            await _userService.DeleteById(userId);

            // Returner en OK-response for at bekræfte, at sletningen er udført
            return Ok();
        }

        [HttpGet("userId")]
        public async Task<IActionResult> FindById([FromRoute] int userId)
        {
            // Hent useren med det angivne ID fra UserService
            return (IActionResult)await _userService.FindById(userId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserModel newUser)
        {
            // Valider inputmodellen ved at tjekke ModelState for valideringsfejl
            if (!ModelState.IsValid)
            {
                // Returner en BadRequest-response med valideringsfejlene, hvis der er nogen
                return BadRequest(ModelState);
            }

            // Opret produktet ved hjælp af ProductService
            await _userService.CreateUser(newUser);

            // Returner et CreatedAtActionResult som svar for at bekræfte oprettelsen
            // inkluderer URL'en til den nye ressource og det oprettede produkt
            return CreatedAtAction(nameof(FindById), new { id = newUser.Id }, newUser);
        }


        [HttpPut("update/{usedId:int}"), Authorize]
        public async Task<IActionResult> UpdateUser(UserModel updateUser)
        {
            var user = await _userService.UpdateUser(updateUser);

            if (user is null)
            {
                return NotFound($"Unable to find user with ID = {updateUser.Id}");
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var auth = await _userService.AuthenticateUser(email, password);

            if (auth is null)
            {
                return Unauthorized("Bad login");
            }

            return Ok(auth);
        }
    }
}
