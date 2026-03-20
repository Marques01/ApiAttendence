namespace Domain.Entities
{
    public class Habilitation
    {
        public int HabilitationId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool Enabled { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime DisabledAt { get; set; }

        // Relacionamento com professores
        public ICollection<TeacherHabilitation> TeacherHabilitations { get; set; } = new List<TeacherHabilitation>();
    }
}
