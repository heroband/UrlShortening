using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly AppDbContext _context;
        public UrlRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Url url)
        {
            await _context.Urls.AddAsync(url);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Url url)
        {
            _context.Urls.Remove(url);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Url>> GetAllAsync()
        {
            return await _context.Urls.ToListAsync();
        }

        public async Task<Url?> GetByIdAsync(string id)
        {
            return await _context.Urls.FindAsync(id);
        }

        public async Task<Url?> GetByShortUrlAsync(string shortUrl)
        {
            return await _context.Urls.FindAsync(shortUrl);
        }
        public async Task<Url?> GetByOriginalUrlAsync(string originalUrl)
        {
            return await _context.Urls.FirstOrDefaultAsync(u => u.OriginalUrl == originalUrl);
        }

        public async Task<bool> IsShortUrlExistsAsync(string shortUrl)
        {
            return await _context.Urls.AnyAsync(u => u.ShortUrl == shortUrl);
        }
    }
}
