using System.ComponentModel.DataAnnotations;

namespace CoreWebApp.Teachers.Models
{
    public class Teacher
    {
        [Key]
        public string Name       { get; set;}
        public string Discipline { get; set;}
    }
}
