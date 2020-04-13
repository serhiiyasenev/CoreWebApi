using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsApi.Models
{
    public class GroupModel
    {
        public int GroupId { get; set; }
        public int Course { get; set; }
        public string GroupName { get; set; }
        public List<TeacherModel> Teachers { get; set; }
        public List<StudentModel> Students { get; set; }
    }
}
