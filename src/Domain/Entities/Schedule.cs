namespace Domain.Entities
{
    public class Schedule
    {
        public int ScheduleId { get; set; }

        public int ClassId { get; set; }

        public int TeacherId { get; set; }

        public DateTime Date { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public bool IsHoliday { get; set; }

        public bool Enabled { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime DisabledAt { get; set; }

        // Relacionamentos
        public Classes Classes { get; set; } = null!;

        public Teacher Teacher { get; set; } = null!;

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
