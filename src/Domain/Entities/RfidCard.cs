namespace Domain.Entities
{
    public class RfidCard
    {
        public int RfidCardId { get; set; }

        public string Code { get; set; } = string.Empty;

        public int Number { get; set; }

        public bool Enabled { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime DisabledAt { get; set; }

        // Navegação
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
