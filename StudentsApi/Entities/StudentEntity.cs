using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsApi.Entities
{
    [Table("StudentEntity")]
    public class StudentEntity
    {
        public int Id         { get; set; }
        public string Name    { get; set; }
        public int    Score   { get; set; }
        public List<StudentTeacherEntity> Teachers { get; set;}
    }
}