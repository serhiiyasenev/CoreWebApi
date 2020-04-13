using LoginApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginApi.Context
{
    public class LoginDbContext : IdentityDbContext<MyUser>
    {

        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options) { }

        public DbSet<MyUser> MyUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<MyUser>().HasKey(user => user.Id);
        }

    }
}