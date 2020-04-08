using System.Collections.Generic;
using StudentsApi.Students.Models;

namespace StudentsApi.Teachers.Models
{
    public class TeacherModel
    {
        public string Name       { get; set;}
        public string Discipline { get; set;}
        public List<StudentModel> Students { get; set; }

    }
}
