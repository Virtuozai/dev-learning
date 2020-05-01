using System.Configuration;
using dev_learning.Extensions;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();
        }

    }
}
