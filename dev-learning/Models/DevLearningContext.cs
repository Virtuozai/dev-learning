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
            modelBuilder.Entity<UserSubject>(entity =>
            {
                entity.HasKey(e => new {
                    e.UserId,
                    e.SubjectId
                });
                entity.HasOne(ot => ot.User)
                    .WithMany(o => o.Subjects)
                    .HasForeignKey(ot => ot.UserId);

                entity.HasOne(ot => ot.Subject)
                    .WithMany(t => t.Users)
                    .HasForeignKey(ot => ot.SubjectId);
            });

            modelBuilder.RemovePluralizingTableNameConvention();
        }

    }
}
