using System.ComponentModel.DataAnnotations;

namespace FirstWebApplication.Models
{
    public class Teacher
    {
        [Key]
        public string Name       { get; set;}
        public string Discipline { get; set;}
    }
}
