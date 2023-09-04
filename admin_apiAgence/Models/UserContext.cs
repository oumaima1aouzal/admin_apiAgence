using Microsoft.EntityFrameworkCore;

namespace admin_apiAgence.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        { }

        public DbSet<user> TableUser { get; set; }
    }
}
