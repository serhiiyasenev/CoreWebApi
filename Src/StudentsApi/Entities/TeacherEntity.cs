using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsApi.Entities
{
    [Table("TeacherEntity")]
    public class TeacherEntity
    {
        [Key]
        public int TeacherId { get; set; }

        public string TeacherName { get; set; }

        public string Discipline  { get; set; }

        public List<GroupEntity> Groups { get; set; }
    }
}
