using dev_learning.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace dev_learning
{
    public class Startup
    {
        readonly string CrossOriginsConfigName = "_crossOriginsConfigName";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<DevLearningContext>(
               options => options.UseMySql(Configuration.GetConnectionString("dbConfig")
            ));

            services.AddCors(options =>
            {
                options.AddPolicy(name: CrossOriginsConfigName,
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:5000")
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyHeader();
                    });
            });


            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
          
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CrossOriginsConfigName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
