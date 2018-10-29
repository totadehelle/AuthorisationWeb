using Microsoft.EntityFrameworkCore;

namespace AuthorizationWeb.Models
{
    public class AuthServiceContext : DbContext
    {
        public AuthServiceContext(DbContextOptions<AuthServiceContext> options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}