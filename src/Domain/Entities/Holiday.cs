namespace Domain.Entities
{
    public class Holiday
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime Day { get; set; }

        public bool IsFixedDate { get; set; }

        public bool National { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
