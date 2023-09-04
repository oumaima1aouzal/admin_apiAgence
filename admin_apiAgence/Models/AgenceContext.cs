using Microsoft.EntityFrameworkCore;

namespace admin_apiAgence.Models
{
    public class AgenceContext : DbContext
    {
        public AgenceContext(DbContextOptions<AgenceContext> options) : base(options)
        { }

        public DbSet<Agence> Agence { get; set; }

    }


}