using Gateway.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;

namespace Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapWhen(context => context.Request.Path.Value != null 
                                   && context.Request.Path.HasValue 
                                   && context.Request.Path.Value.StartsWith("/static/"),
                appBuilder =>
                { 
                    appBuilder.Run(async context =>
                    {
                        if (context.Request.Path.Value != null)
                        {
                            var fileName = context.Request.Path.Value["/static/".Length..];

                            Console.WriteLine($"Returning content path: {fileName}");

                            var currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Static");

                            var filePath = Directory.GetFiles(currentDirectory, fileName + ".*").FirstOrDefault();

                            if (string.IsNullOrEmpty(filePath))
                            {
                                await context.Response.WriteAsync($"No files with name '{fileName}'");
                                return;
                            }

                            var file = await File.ReadAllBytesAsync(filePath);

                            Console.WriteLine($"Returning content file length: {file.Length}");

                            await context.Response.Body.WriteAsync(file);
                        }
                    });
                });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello Gateway!");
                });

                endpoints.MapControllers();
            });



            var router = new Router("routes.json");

            app.Run(async (context) =>
            {
                var content = await router.RouteRequest(context.Request);
                await context.Response.WriteAsync(await content.Content.ReadAsStringAsync());
            });

        }
    }
}
