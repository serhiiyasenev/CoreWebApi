using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentsApi.Entities
{
    public class GroupTeacherEntity
    {
        public int GroupId { get; set; }
        public int TeacherId { get; set; }

        public GroupEntity Group { get; set; }

        public TeacherEntity Teacher { get; set; }
    }
}
