using Microsoft.EntityFrameworkCore;
using StudentsApi.Entities;

namespace StudentsApi.Contexts
{
    public class StudentsDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<DisciplineEntity> Disciplines { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<StudentEntity>().HasMany(d => d.Disciplines);
        }
    }
}