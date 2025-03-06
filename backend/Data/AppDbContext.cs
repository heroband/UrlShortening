using backend.Entities;
using backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){ }

        public DbSet<Url> Urls { get; set; }
        public DbSet<Algorithm> Algorithms { get; set; }
    }
}
