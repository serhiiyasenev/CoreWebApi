using System.Collections.Generic;
using StudentsApi.Teachers.Models;

namespace StudentsApi.Students.Models
{
    public class StudentModel
    {
        public string Name { get; set; }
        public int    Score   { get; set; }
        public List<TeacherModel>  Teachers { get; set;}
    }
}