using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentsApi.Contexts;
using StudentsApi.Entities;

namespace StudentsApi
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
            services.AddControllers();

            services.AddDbContext<StudentsDbContext>(options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString("MyConnectionString"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, StudentsDbContext dbContext)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();


            app.MapWhen(context => context.Request.Path.HasValue &&
            context.Request.Path.Value.StartsWith("/static/"),
            a =>
            {
                a.Run(async context =>
                {
                    if (context.Request.Path.Value != null)
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
                    }
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello Students!");
                });

                endpoints.MapControllers();
            });

            dbContext.Database.Migrate();

            //Examples(dbContext);


        }

        private void Examples(StudentsDbContext dbContext)
        {
            var chemistryTeacher = new TeacherEntity
            {
                Name = "Ivan",
                Discipline = "Chemistry"
            };

            var serhii = new StudentEntity
            {
                Name = "Serhii",
                Score = 99
            };

            var petro = new StudentEntity
            {
                Name = "Petro",
                Score = 98
            };


            dbContext.AddRange(serhii, petro, chemistryTeacher);


            chemistryTeacher.Students = new List<StudentTeacherEntity>
            {
                new StudentTeacherEntity {Teacher = chemistryTeacher, Student = serhii},
                new StudentTeacherEntity {Teacher = chemistryTeacher, Student = petro}

            };

            dbContext.SaveChanges();
        }
    }
}
