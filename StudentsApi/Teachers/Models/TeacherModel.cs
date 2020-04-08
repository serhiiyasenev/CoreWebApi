using System.Collections.Generic;
using StudentsApi.Students.Models;

namespace StudentsApi.Teachers.Models
{
    public class TeacherModel
    {
        public string Name       { get; set;}
        public string Discipline { get; set;}

        public IEnumerable<StudentModel> Students { get; set; }

    }
}
