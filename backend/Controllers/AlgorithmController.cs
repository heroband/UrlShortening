using backend.Dtos;
using backend.Entities;
using backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlgorithmController : ControllerBase
    {
        private readonly IAlgorithmRepository _algorithmRepository;
        public AlgorithmController(IAlgorithmRepository algorithmRepository)
        {
            _algorithmRepository = algorithmRepository;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAlgorithmInfo()
        {
            var algohithmInfo = await _algorithmRepository.GetAlgorithmInfoAsync();
            return Ok(new { message = "Algorithm description retrieved successfully.", algohithmInfo });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAboutInfo(AlgorithmDto algorithmDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exists = await _algorithmRepository.IsAlgorithmInfoExistsAsync();
            if (exists)
            {
                return Conflict(new { message = "Algorithm description already exists." });
            }

            await _algorithmRepository.CreateAlgorithmInfoAsync(algorithmDto.Description);
            return Ok(new { message = "Algorithm information created successfully." });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAlgorithmInfo(AlgorithmDto algorithmDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _algorithmRepository.UpdateAlgorithmInfoAsync(algorithmDto.Description);
            return Ok(new { message = "Algorithm description updated successfully." });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAlgorithmInfo()
        {
            var exists = await _algorithmRepository.IsAlgorithmInfoExistsAsync();
            if (!exists)
            {
                return NotFound(new { message = "No algorithm description found to delete." });
            }

            await _algorithmRepository.DeleteAlgorithmInfoAsync();
            return Ok(new { message = "Algorithm description deleted successfully." });
        }
    }
}
