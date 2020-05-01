using System.Configuration;
using Microsoft.EntityFrameworkCore;
namespace dev_learning.Models
{
    public class DevLearningContext : DbContext
    {
        public DevLearningContext(DbContextOptions<DevLearningContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }

    }
}
