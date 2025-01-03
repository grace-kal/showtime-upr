using Microsoft.EntityFrameworkCore;
using ShowTimeUpr.Models;

namespace ShowTimeUpr.DataAccess
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
