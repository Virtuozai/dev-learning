using System.Configuration;
using Microsoft.EntityFrameworkCore;
namespace dev_learning.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

    }
}
