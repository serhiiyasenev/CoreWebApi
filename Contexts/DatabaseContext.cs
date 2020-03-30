using FirstWebApplication.Authorization.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApplication.Models
{
    public class DatabaseContext : IdentityDbContext<MyUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

    }
}