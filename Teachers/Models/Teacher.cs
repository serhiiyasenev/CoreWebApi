using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebApp.Teachers.Models
{
    [Table("Teacher")]
    public class Teacher
    {
        public int Id { get; set; }
        public string Name       { get; set;}
        public string Discipline { get; set;}
    }
}
