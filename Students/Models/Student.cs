using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreWebApp.Teachers.Models;

namespace CoreWebApp.Students.Models
{
    public class Student
    {
        [Key]
        public string Name { get; set; }
        public int    Score   { get; set; }
        public IEnumerable<Teacher>  Teachers { get; set;}
    }
}