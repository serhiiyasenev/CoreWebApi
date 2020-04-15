using System.Collections.Generic;
using Newtonsoft.Json;

namespace StudentsApi.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public List<DisciplineModel> Disciplines { get; set; }
    }
}