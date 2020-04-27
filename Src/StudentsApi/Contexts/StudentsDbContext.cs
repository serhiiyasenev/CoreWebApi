using Microsoft.EntityFrameworkCore;
using StudentsApi.Entities;

namespace StudentsApi.Contexts
{
    public class StudentsDbContext : DbContext
    {
        public StudentsDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<StudentEntity> Students { get; set; }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<StudentEntity>().HasMany<DisciplineEntity>();

        }
    }
}