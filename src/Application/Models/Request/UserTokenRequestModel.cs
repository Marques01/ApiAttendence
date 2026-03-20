namespace Application.Models.Request
{
    public class UserTokenRequestModel
    {
        public string Token { get; set; } = string.Empty;
        
        public string RefreshToken { get; set; } = string.Empty;
    }
}
