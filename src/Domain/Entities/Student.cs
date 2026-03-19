namespace Domain.Entities
{
    public class Student
    {
        public int StudentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Registration { get; set; } = string.Empty;

        public bool Enabled { get; set; }

        public int RfidCardId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime DisabledAt { get; set; }
    }
}
