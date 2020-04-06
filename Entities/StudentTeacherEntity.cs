using System.ComponentModel.DataAnnotations.Schema;
using CoreWebApp.Students.Models;
using CoreWebApp.Teachers.Models;

namespace CoreWebApp.Entities
{
    [Table("StudentTeacher")]
    public class StudentTeacherEntity
    {
        public int StudentId { get; set; }

        public int TeacherId { get; set; }

        public StudentEntity Student { get; set; }

        public TeacherEntity Teacher { get; set; }
    }
}
