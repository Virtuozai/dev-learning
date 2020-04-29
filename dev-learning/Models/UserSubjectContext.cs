using Microsoft.EntityFrameworkCore;

namespace dev_learning.Models
{
    public class UserSubjectContext : DbContext
    {    
        public UserSubjectContext(DbContextOptions<UserSubjectContext> options)
            : base(options)
        {
        }
       

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



        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSubject> UserSubjects { get; set; }


    }
}
