using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using FirstWebApplication.Authorization.Models;
using FirstWebApplication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWebApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMvcCore().AddApiExplorer();
            services.AddLogging();
            services.AddControllers();

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString("MyConnectionString"));
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
            });

            services.Configure<PasswordHasherOptions>(options =>
            {
                options.IterationCount = 5000;
            });

            services.AddIdentity<MyUser, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>();

            services.AddSingleton<JwtSecurityToken>();



        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            //app.MapWhen(context => context.Request.Path.HasValue &&
            //                       context.Request.Path.StartsWithSegments("/student/"),
            //    a => a.UseMiddleware<StudentMiddleware>());

            app.MapWhen(context => context.Request.Path.HasValue &&
            context.Request.Path.Value.StartsWith("/static/"),
            a =>
            {
                a.Run(async context =>
                {
                    var fileName = context.Request.Path.Value.Substring(8);

                    Console.WriteLine($"Returning content path: {fileName}");

                    var currentDirectory = Directory.GetCurrentDirectory();
                    
                    var filePathCombine = Path.Combine(currentDirectory, "Static", fileName);

                    if (!File.Exists(filePathCombine))
                    {
                        return;
                    }

                    var file = await File.ReadAllBytesAsync(filePathCombine);

                    Console.WriteLine($"Returning content file length: {file.Length}");

                    await context.Response.Body.WriteAsync(file);
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapControllers();
            });
        }
    }
}
