using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StudentsApi.Contexts;

namespace StudentsApi
{
    public class Startup(IConfiguration configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // Register DbContext for all environments except Testing (where test infrastructure provides in-memory DB)
            if (env != "Testing")
            {
                services.AddDbContext<StudentsDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("StudentsDb")));
            }

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Students API" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, StudentsDbContext dbContext)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                dbContext.Database.Migrate();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello Students!");
                });

                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Students API");
            });

        }
    }
}