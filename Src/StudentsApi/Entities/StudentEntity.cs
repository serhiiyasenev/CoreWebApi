using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsApi.Entities
{
    [Table(nameof(StudentEntity))]
    public class StudentEntity
    {
        [Key]
        public int StudentId         { get; set; }
        public string StudentName    { get; set; }
        public List<DisciplineEntity> Disciplines { get; set; }
    }
}