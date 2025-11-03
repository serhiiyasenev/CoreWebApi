namespace StudentsApi.Entities
{
    public class DisciplineEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StudentEntityId { get; set; }
    }
}
