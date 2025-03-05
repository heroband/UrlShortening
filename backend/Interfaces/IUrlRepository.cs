using backend.Models;

namespace backend.Interfaces
{
    public interface IUrlRepository
    {
        Task<Url?> GetByIdAsync(string id);
        Task<IEnumerable<Url>> GetAllAsync();
        Task AddAsync(Url urlModel);
        Task DeleteAsync(Url urlModel);

    }
}
