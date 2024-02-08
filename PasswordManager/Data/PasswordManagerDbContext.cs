using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;

namespace PasswordManager.Data
{
    public class PasswordManagerDbContext : DbContext
    {
        public PasswordManagerDbContext(DbContextOptions<PasswordManagerDbContext> options) : base(options)
        {
        }

        public DbSet<PasswordStoreItem> PasswordStoreItems { get; set; }
    }
}
