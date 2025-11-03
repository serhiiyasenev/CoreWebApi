using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentsApi;
using StudentsApi.Contexts;
using System.Linq;

namespace StudentsApiTest
{
    public class StudentsApiWebFactory : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<StudentsDbContext>));
                if (dbContextDescriptor != null)
                    services.Remove(dbContextDescriptor);

                var contextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(StudentsDbContext));
                if (contextDescriptor != null)
                    services.Remove(contextDescriptor);

                services.AddDbContext<StudentsDbContext>(options =>
                {
                    options.UseInMemoryDatabase("StudentsTestDb");
                });

                // Database initialization moved to CreateHost override.
            });
        }
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = base.CreateHost(builder);
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<StudentsDbContext>();
                db.Database.EnsureCreated();
            }
            return host;
        }
    }
}
