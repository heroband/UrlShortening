using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Interfaces
{
    public interface IUrlRepository
    {
        Task<Url?> GetByIdAsync(string id);
        Task<IEnumerable<Url>> GetAllAsync();
        Task AddAsync(Url urlModel);
        Task DeleteAsync(Url urlModel);
        Task<Url?> GetByShortUrlAsync(string shortUrl);
        Task<Url?> GetByOriginalUrlAsync(string originalUrl);
        Task<bool> IsShortUrlExistsAsync(string shortUrl);
    }
}
