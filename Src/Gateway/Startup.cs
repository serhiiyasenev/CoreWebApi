using Gateway.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

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

            app.MapWhen(context => context.Request.Path.HasValue &&
                                   context.Request.Path.Value.StartsWith("/static/"),
                a =>
                {
                    a.Run(async context =>
                    {
                        if (context.Request.Path.Value != null)
                        {
                            var fileName = context.Request.Path.Value[8..];

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
