using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace dev_learning.Models
{
    public class TestModelContext: DbContext
    {
        public TestModelContext(DbContextOptions<TestModelContext> options)
            : base(options)
        {
        }

        public DbSet<TestModel> TestModelList { get; set; }
        
    }
}
