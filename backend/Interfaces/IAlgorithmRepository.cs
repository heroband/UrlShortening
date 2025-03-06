using backend.Entities;

namespace backend.Interfaces
{
    public interface IAlgorithmRepository
    {
        Task<Algorithm> GetAlgorithmInfoAsync();
        Task<bool> IsAlgorithmInfoExistsAsync();
        Task CreateAlgorithmInfoAsync(string description);
        Task UpdateAlgorithmInfoAsync(string description);
        Task DeleteAlgorithmInfoAsync();
    }
}
