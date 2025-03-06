using backend.Entities;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repositories
{
    public class AlgorithmRepository : IAlgorithmRepository
    {
        private readonly AppDbContext _context;
        public AlgorithmRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAlgorithmInfoAsync(string description)
        {
            var algorithm = new Algorithm { Description = description };
            _context.Algorithms.Add(algorithm);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAlgorithmInfoAsync()
        {
            var algorithm = await _context.Algorithms.FirstOrDefaultAsync();
            if (algorithm != null)
            {
                _context.Algorithms.Remove(algorithm);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Algorithm> GetAlgorithmInfoAsync()
        {
            return await _context.Algorithms.FirstOrDefaultAsync() ?? new Algorithm { Description = "No information available yet." };
        }

        public async Task<bool> IsAlgorithmInfoExistsAsync()
        {
            return await _context.Algorithms.AnyAsync();
        }

        public async Task UpdateAlgorithmInfoAsync(string description)
        {
            var existingAlgorithm = await _context.Algorithms.FirstOrDefaultAsync();
            if(existingAlgorithm == null)
            {
                throw new InvalidOperationException("Cannot update. Algorithm information does not exist.");
            }

            existingAlgorithm.Description = description;
            _context.Update(existingAlgorithm);
            await _context.SaveChangesAsync();
        }
    }
}
