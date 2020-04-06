using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreWebApp.Entities;

namespace CoreWebApp.Teachers.Models
{
    public class TeacherEntity
    {
        [Key]
        public int TeacherId { get; set; }
        public string Name       { get; set;}
        public string Discipline { get; set;}
        public IEnumerable<StudentTeacherEntity> Students { get; set; }
    }
}
