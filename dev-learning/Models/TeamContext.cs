using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace dev_learning.Models
{
    class TeamContext : DbContext
    {
        public TeamContext(DbContextOptions<TeamContext> options)
            :   base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
    }
}
