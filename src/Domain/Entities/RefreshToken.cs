namespace Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime CreatAt { get; set; }

        public DateTime Expiration { get; set; }
    }
}
