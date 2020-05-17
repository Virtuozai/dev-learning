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

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSubject> UserSubjects { get; set; }
        public DbSet<Team> Teams { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();
        }

    }
}
