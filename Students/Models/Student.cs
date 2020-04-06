using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreWebApp.Teachers.Models;

namespace CoreWebApp.Students.Models
{
    [Table("Student")]
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int    Score   { get; set; }
        public IEnumerable<Teacher>  Teachers { get; set;}
    }
}