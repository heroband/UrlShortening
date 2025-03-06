using backend.Dtos;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUrlShorteningService _urlShorteningService;

        public UrlController(IUrlRepository urlRepository, IUserRepository userRepository, IUrlShorteningService urlShorteningService)
        {
            _urlRepository = urlRepository;
            _userRepository = userRepository;
            _urlShorteningService = urlShorteningService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUrl(ShortUrlDto shortUrlDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUrl = await _urlRepository.GetByOriginalUrlAsync(shortUrlDto.OriginalUrl);
            if (existingUrl != null)
            {
                return Conflict(new { message = "This URL has already been shortened." });
            }

            var user = await _userRepository.GetUserByClaimsPrincipalAsync(User);

            string shortUrl;
            do
            {
                shortUrl = _urlShorteningService.GenerateUrlCode();
            }
            while (await _urlRepository.IsShortUrlExistsAsync(shortUrl));

            var url = new Url
            {
                OriginalUrl = shortUrlDto.OriginalUrl,
                ShortUrl = shortUrl,
                CreatedBy = user.Id,
                CreatedAt = DateTime.UtcNow
            };

            await _urlRepository.AddAsync(url);
            return Ok(new { message = "Url was successfully shortened", url });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var urls = await _urlRepository.GetAllAsync();
            return Ok(urls);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUrlById(string id)
        {
            var url = await _urlRepository.GetByIdAsync(id);
            if (url == null)
            {
                return NotFound(new { message = "Url wasn't found" });
            }
            return Ok(new { message = "Url was found", url });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUrl(string id)
        {
            var url = await _urlRepository.GetByIdAsync(id);
            if (url == null)
            {
                return NotFound(new { message = "Url wasn't found" });
            }

            var user = await _userRepository.GetUserByClaimsPrincipalAsync(User);
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && url.CreatedBy != user.Id)
            {
                return Forbid();
            }

            await _urlRepository.DeleteAsync(url);
            return Ok(new { message = "Url was successfully deleted" });
        }

        [HttpGet("short/{shortUrl}")]
        public async Task<IActionResult> RedirectToOriginal(string shortUrl)
        {
            var url = await _urlRepository.GetByShortUrlAsync(shortUrl);
            if (url == null)
            {
                return NotFound(new { message = "Url wasn't found" });
            }
            return Ok(new { message = $"{url.OriginalUrl}" });
        }
    }
}
