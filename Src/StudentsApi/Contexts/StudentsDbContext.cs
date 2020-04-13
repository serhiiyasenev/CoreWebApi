using Microsoft.EntityFrameworkCore;
using StudentsApi.Entities;

namespace StudentsApi.Contexts
{
    public class StudentsDbContext : DbContext
    {
        public StudentsDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<StudentEntity> Students { get; set; }

        public DbSet<TeacherEntity> Teachers { get; set; }

        public DbSet<GroupEntity> Groups { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<GroupEntity>()
                .HasMany(g => g.Students)
                .WithOne(s => s.Group);

            builder.Entity<GroupEntity>()
                .HasMany(g => g.Teachers)
                .With(t => t.Groups);

            builder.Entity<TeacherEntity>()
                .HasMany(t => t.Groups);
        }

    }
}