using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsApi.Entities
{
    [Table("TeacherEntity")]
    public class TeacherEntity
    {
        public int Id            { get; set; }
        public string Name       { get; set;}
        public string Discipline { get; set;}
        public List<StudentTeacherEntity> Students { get; set; }
    }
}
