using System.Security.Claims;
using backend.Dtos;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public AccountController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
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

            var roles = await _userRepository.GetUserRolesAsync(user);
            var token = _tokenService.CreateToken(user, roles);
            return Ok(new { message = "User registered successfully.", token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetUserByUsernameAsync(model.Username);
            if(user == null)
            {
                return Unauthorized(new { message = "Invalid username!" });
            }

            var signInResult = await _userRepository.CheckPassword(user, model.Password);
            if (!signInResult.Succeeded)
            {
                return Unauthorized(new { message = "Invalid password!" });
            }

            var roles = await _userRepository.GetUserRolesAsync(user);

            var token = _tokenService.CreateToken(user, roles);
            return Ok(new { message = "Login successfully.", token });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var user = await _userRepository.GetUserByClaimsPrincipalAsync(User);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid token!" });
            }

            return Ok(new { message = "Token is valid!", userName = user.UserName, email = user.Email });
        }
    }
}
