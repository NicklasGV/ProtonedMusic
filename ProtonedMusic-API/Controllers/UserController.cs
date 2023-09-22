﻿using Microsoft.AspNetCore.Mvc;

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
            // Hent alle produkter fra ProductService
            return (IActionResult)await _userService.GetAll();
        }

        [HttpDelete("userId")]
        public async Task<IActionResult> DeleteById([FromRoute] int userId)
        {
            // Hent produktet med det angivne ID fra ProductService
            var user = await _userService.FindById(userId);

            // Hvis produktet ikke findes, returner en 404 Not Found-response
            if (user == null)
            {
                return NotFound(); // Returnerer 404 hvis produktet ikke findes..
            }

            // Slet produktet med det angivne ID fra ProductService
            await _userService.DeleteById(userId);

            // Returner en OK-response for at bekræfte, at sletningen er udført
            return Ok();
        }

        [HttpGet("userId")]
        public async Task<IActionResult> FindById([FromRoute] int userId)
        {
            // Hent produktet med det angivne ID fra ProductService
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


        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateById([FromRoute] int userId, [FromBody] UserModel updateUser)
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
    }
}
