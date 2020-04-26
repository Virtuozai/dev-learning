using System.Configuration;
using Microsoft.EntityFrameworkCore;
namespace dev_learning.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
