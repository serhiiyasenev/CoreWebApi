using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreWebApp.Entities;

namespace CoreWebApp.Students.Models
{
    public class StudentEntity
    {
        [Key]
        public int StudentId { get; set; }
        public string Name    { get; set; }
        public int    Score   { get; set; }

        public IEnumerable<StudentTeacherEntity> Teachers { get; set;}
    }
}