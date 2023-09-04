using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace admin_apiAgence.Models
{
    public class adminContext : DbContext
    {
        public adminContext(DbContextOptions<adminContext> options) : base(options)
        { }

        public DbSet<admin> TableAdmin { get; set; }
    }
}

