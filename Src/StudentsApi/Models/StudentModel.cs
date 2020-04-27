using System.Collections.Generic;
using Newtonsoft.Json;

namespace StudentsApi.Models
{
    public class StudentModel
    {
        public StudentModel()
        {
            Disciplines = new HashSet<DisciplineModel>();
        }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public HashSet<DisciplineModel> Disciplines { get; set; }
    }
}