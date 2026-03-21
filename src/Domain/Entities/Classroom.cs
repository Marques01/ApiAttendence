namespace Domain.Entities
{
    public class Classroom
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public string Location { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Relacionamento
        public ICollection<Classes> Classes { get; set; } = new List<Classes>();
    }
}
