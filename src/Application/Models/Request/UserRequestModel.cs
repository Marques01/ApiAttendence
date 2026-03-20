namespace Application.Models.Request
{
    public record UserRequestModel
    {
        public string Login { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
