using System.Text;
using LoginApi.Context;
using LoginApi.Models;
using LoginApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace LoginApi
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

            services.AddDbContext<LoginDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("MyConnectionString")));


            services.Configure<IdentityOptions>(options =>
                                                {
                                                    options.Password.RequireLowercase = true;
                                                    options.Password.RequireUppercase = true;
                                                    options.Password.RequireNonAlphanumeric = false;
                                                    options.Password.RequireDigit = true;
                                                    options.Password.RequiredLength = 10;
                                                });

            services.Configure<PasswordHasherOptions>(option =>
                                                      {
                                                          option.IterationCount = 120000;
                                                      });

            services.AddIdentity<MyUser, IdentityRole>()
                    .AddEntityFrameworkStores<LoginDbContext>()
                    .AddDefaultTokenProviders();

            services.AddSingleton<JwtService>();

            var secret = _configuration.GetSection("JwtConfig" + ":Secret").Value;

            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(configureOptions =>
                                       {
                                           configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                           configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                       })
                    .AddJwtBearer(jwtBearerOptions =>
                                  {
                                      jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                                      {
                                          IssuerSigningKey = new SymmetricSecurityKey(key),
                                          ValidateIssuer = false,
                                          ValidateAudience = false
                                      };
                                  });

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Login API" });
            });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, LoginDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello LoginApi!");
                });
                endpoints.MapControllers();
            });

            dbContext.Database.Migrate();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Login API");
            });
        }
    }
}
