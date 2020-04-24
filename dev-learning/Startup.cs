using dev_learning.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace dev_learning
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
       /* public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            _ = services.AddDbContext<Models.UserContext>(opt =>
                opt.UseInMemoryDatabase("Users"));
        }*/

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<UserContext>(
               options => options.UseMySql(Configuration.GetConnectionString("dbConfig")
            ));
            services.AddMvc();

            services.AddDbContext<DbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("dev_learningContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
