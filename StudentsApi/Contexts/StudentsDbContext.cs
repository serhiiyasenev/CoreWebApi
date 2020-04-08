using Microsoft.EntityFrameworkCore;
using StudentsApi.Entities;

namespace StudentsApi.Contexts
{
    public class StudentsDbContext : DbContext
    {
        public StudentsDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<StudentEntity> Students { get; set; }

        public DbSet<TeacherEntity> Teachers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StudentEntity>().HasMany(v => v.Teachers);
            
            builder.Entity<TeacherEntity>().HasMany(v => v.Students);

            builder.Entity<StudentTeacherEntity>()
                .HasKey(st => new { st.StudentId, st.TeacherId });

            builder.Entity<StudentTeacherEntity>()
                .HasOne(st => st.Student)
                .WithMany(s => s.Teachers)
                .HasForeignKey(st => st.StudentId);

            builder.Entity<StudentTeacherEntity>()
                .HasOne(st => st.Teacher)
                .WithMany(s => s.Students)
                .HasForeignKey(st => st.TeacherId);
        }

    }
}