﻿using System.Security.Claims;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> IsEmailTakenAsync(string email);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<SignInResult> CheckPassword(User user, string password);
        Task<User?> GetUserByClaimsPrincipalAsync(ClaimsPrincipal userPrincipal);
        Task<List<string>> GetUserRolesAsync(User user);
    }
}
