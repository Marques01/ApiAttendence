namespace Domain.Entities
{
    public class TeacherHabilitation
    {
        public int TeacherHabilitationId { get; set; }

        public int TeacherId { get; set; }

        public int HabilitationId { get; set; }

        public bool Enabled { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime DisabledAt { get; set; }

        // Relacionamentos
        public Teacher Teacher { get; set; } = null!;

        public Habilitation Habilitation { get; set; } = null!;
    }
}
