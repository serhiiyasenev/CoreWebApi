using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebApp.Entities
{
    [Table("TeacherEntity")]
    public class TeacherEntity
    {
        [Key]
        public int TeacherId     { get; set; }
        public string Name       { get; set;}
        public string Discipline { get; set;}
        [ForeignKey("Students")]
        public IEnumerable<StudentTeacherEntity> Students { get; set; }
    }
}
