using CoreWebApp.Authorization.Models;
using CoreWebApp.Entities;
using CoreWebApp.Students.Models;
using CoreWebApp.Teachers.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApp.Contexts
{
    public class DatabaseContext : IdentityDbContext<MyUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">
        /// The builder being used to construct the model for this context.
        /// </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StudentTeacherEntity>()
                .HasKey(st => new {st.StudentId, st.TeacherId});


            builder.Entity<StudentTeacherEntity>()
                .HasOne(st => st.Student)
                .WithMany(st => st.Teachers)
                .HasForeignKey(st => st.StudentId);

            builder.Entity<StudentTeacherEntity>()
                .HasOne(st => st.Teacher)
                .WithMany(st => st.Students)
                .HasForeignKey(st => st.TeacherId);
        }

    }
}