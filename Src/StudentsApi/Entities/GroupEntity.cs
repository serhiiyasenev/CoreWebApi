using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentsApi.Models;

namespace StudentsApi.Entities
{
    [Table("GroupEntity")]
    public class GroupEntity
    {
        [Key]
        public int GroupId { get; set; }
        public int Course { get; set; }
        public string GroupName { get; set; }
        public List<TeacherEntity> Teachers { get; set; }
        public List<StudentEntity> Students { get; set; }
    }
}
