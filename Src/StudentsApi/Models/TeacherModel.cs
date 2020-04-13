using System.Collections.Generic;
using Newtonsoft.Json;

namespace StudentsApi.Models
{
    [JsonConverter(typeof(TeacherModel))]
    public class TeacherModel
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set;}
        public string Discipline { get; set;}
        public List<GroupModel> Groups { get; set;}
}
}
