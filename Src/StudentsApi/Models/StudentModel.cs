using System.Collections.Generic;
using Newtonsoft.Json;

namespace StudentsApi.Models
{
    [JsonConverter(typeof(StudentModel))]
    public class StudentModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int Course { get; set; }
        public int    AvrScore   { get; set; }
        public string GroupName { get; set; }
    }
}