using Domain.Enum;

namespace Domain.Entities
{
    public class RegisterLog
    {
        public long Id { get; set; }

        public string Message { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;

        public string Origin { get; set; } = string.Empty;

        public string Exception { get; set; } = string.Empty;

        public string StackTrace { get; set; } = string.Empty;

        public string Inner { get; set; } = string.Empty;

        public SituationEnum Situation { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
