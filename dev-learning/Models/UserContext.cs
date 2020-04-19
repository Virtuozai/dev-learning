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

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbConfig"].ConnectionString;

            _ = optionsBuilder.UseMySQL(connectionString);
        }
    }
}