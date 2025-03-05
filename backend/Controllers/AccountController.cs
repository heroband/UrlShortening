using backend.Dtos;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userRepository.IsEmailTakenAsync(model.Email))
            {
                return BadRequest(new { message = "Email is already taken." });
            }

            var user = new User { UserName = model.Username, Email = model.Email };

            var result = await _userRepository.AddUserAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "User registration failed", errors = result.Errors.Select(e => e.Description) });
            }
            return Ok(new { message = "User registered successfully." });
        }
    }
}
