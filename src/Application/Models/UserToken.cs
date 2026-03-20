using System.Text.Json.Serialization;

namespace Application.Models
{
    public record UserToken
    {
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; init; }

        [JsonPropertyName("message")]
        public string Message { get; init; } = string.Empty;

        [JsonPropertyName("token")]
        public string Token { get; init; } = string.Empty;

        [JsonPropertyName("expiration")]
        public DateTime Expiration { get; init; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; } = string.Empty;

        [JsonPropertyName("refreshTokenExpiration")]
        public DateTime RefreshTokenExpiration { get; set; }

        [JsonPropertyName("creatAt")]
        public DateTime CreatAt { get; set; }
    }
}
