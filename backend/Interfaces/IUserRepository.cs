using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> IsEmailTakenAsync(string email);
    }
}
