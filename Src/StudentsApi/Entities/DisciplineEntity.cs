using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsApi.Entities
{
    [Table(nameof(DisciplineEntity))]
    public class DisciplineEntity
    {
        [Key]
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }
    }
}