namespace Application.Models.Request
{
    public class RefreshTokenRequestModel
    {
        public string Token { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
    }
}
