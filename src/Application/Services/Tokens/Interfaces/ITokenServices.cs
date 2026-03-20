using System.Security.Claims;
using Application.Models;
using Domain.Entities;

namespace Application.Services.Tokens.Interfaces
{
    public interface ITokenServices
    {
        UserToken GenerateToken(IEnumerable<Claim> claims, bool rememberMe);

        Task<IEnumerable<Claim>> GetUserRolesAsync(User user);

        Task<UserToken> RefreshTokenAsync(string token, string refreshToken);
    }
}
