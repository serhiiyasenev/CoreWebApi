using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsApi.Entities
{
    public class StudentEntity
    {
        [Key]
        public int Id         { get; set; }
        public string Name    { get; set; }
        public HashSet<DisciplineEntity> Disciplines { get; set; }
    }
}