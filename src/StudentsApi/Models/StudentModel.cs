using StudentsApi.Entities;
using System.Collections.Generic;

namespace StudentsApi.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<DisciplineEntity> Disciplines { get; set; }
    }
}
