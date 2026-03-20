namespace Domain.Entities
{
    public class Teacher
    {
        public int TeacherId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Registration { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool Enabled { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime DisabledAt { get; set; }

        // Relacionamento com habilidades
        public ICollection<TeacherHabilitation> TeacherHabilitations { get; set; } = new List<TeacherHabilitation>();
    }
}
