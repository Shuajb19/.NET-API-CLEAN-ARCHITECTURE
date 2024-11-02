using Domain.Entities; // Make sure this includes your Transaction entity
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Existing DbSet for ApplicationUser
        public DbSet<ApplicationUser> Users { get; set; }

        // DbSet for Account entity
        public DbSet<Account> Accounts { get; set; }

        // New DbSet for Transaction entity
        public DbSet<Transaction> Transactions { get; set; } // Add this line

    }
}
