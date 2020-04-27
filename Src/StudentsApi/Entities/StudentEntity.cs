using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsApi.Entities
{
    [Table(nameof(StudentEntity))]
    public class StudentEntity
    {
        public StudentEntity()
        {
            Disciplines = new HashSet<DisciplineEntity>();
        }

        [Key]
        public int StudentId         { get; set; }
        public string StudentName    { get; set; }
        public HashSet<DisciplineEntity> Disciplines { get; set; }
    }
}