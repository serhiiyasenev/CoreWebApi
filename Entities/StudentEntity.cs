using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebApp.Entities
{
    [Table("StudentEntity")]
    public class StudentEntity
    {
        [Key]
        public int StudentId { get; set; }
        public string Name    { get; set; }
        public int    Score   { get; set; }
        [ForeignKey("Teachers")]
        public IEnumerable<StudentTeacherEntity> Teachers { get; set;}
    }
}