using System.Collections.Generic;

namespace StudentsApi.Models
{
    public class StudentModel
    {
        public string Name { get; set; }
        public HashSet<string> Disciplines { get; set; }
    }
}
