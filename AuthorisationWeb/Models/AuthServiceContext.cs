using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationWeb.Models
{
    public class AuthServiceContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=AuthServiceData.db");
        }
    }
}