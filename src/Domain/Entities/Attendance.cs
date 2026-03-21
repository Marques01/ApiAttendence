namespace Domain.Entities
{
    public class Attendance
    {
        public int Id { get; set; }

        public int ScheduleId { get; set; }

        public int StudentId { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Relacionamentos
        public Schedule Schedule { get; set; } = null!;

        public Student Student { get; set; } = null!;
    }
}
