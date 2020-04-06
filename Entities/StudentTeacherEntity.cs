using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebApp.Entities
{
    [Table("StudentTeacherEntity")]
    public class StudentTeacherEntity
    {
        public int StudentId         { get; set; }
        public int TeacherId         { get; set; }
        public StudentEntity Student { get; set; }
        public TeacherEntity Teacher { get; set; }
    }
}
