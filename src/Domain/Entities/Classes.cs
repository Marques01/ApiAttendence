namespace Domain.Entities
{
    public class Classes
    {
        public int ClassId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int TeacherId { get; set; }

        public int ClassroomId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Notes { get; set; } = string.Empty;

        public bool Enabled { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Relacionamentos
        public Teacher Teacher { get; set; } = null!;

        public Classroom Classroom { get; set; } = null!;

        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
