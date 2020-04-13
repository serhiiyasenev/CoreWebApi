using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentsApi.Models;

namespace StudentsApi.Entities
{
    [Table("StudentEntity")]
    public class StudentEntity
    {
        [Key]
        public int StudentId         { get; set; }
        public string StudentName    { get; set; }
        public int    AvrScore   { get; set; }
        public GroupEntity Group { get; set;}
    }
}