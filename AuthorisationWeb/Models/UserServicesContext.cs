using Microsoft.EntityFrameworkCore;

namespace AuthorisationWeb.Models
{
    public class UserServicesContext : DbContext
    {
        public UserServicesContext(DbContextOptions<UserServicesContext> options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}