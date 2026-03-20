using Application.Models;
using Application.Services.Tokens.Interfaces;
using Domain.Entities;
using Domain.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services.Tokens
{
    public class TokenServices : ITokenServices
    {
        private readonly IUnitOfWork _uof;

        private readonly string _jwtKey;

        public TokenServices(IUnitOfWork uof, IConfiguration configuration)
        {
            _jwtKey = configuration.GetSection("Jwt:Key").Value ?? throw new InvalidOperationException(
                "Não possível encontrar a chave do JWT. Verifique os arquivos de configuração e tente novamente.");

            _uof = uof;
        }

        public UserToken GenerateToken(IEnumerable<Claim> claims, bool rememberMe)
        {
            try
            {
                SigningCredentials creds =
                    new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
                        SecurityAlgorithms.HmacSha256);

                DateTime expiration = DateTime.UtcNow.AddHours(2);

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expiration,
                    signingCredentials: creds);

                if (rememberMe)
                {
                    var refreshToken = GenerateRefreshToken();

                    return new UserToken()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = expiration,
                        Message = "token de acesso gerado com sucesso.",
                        IsSuccess = true,
                        RefreshToken = refreshToken.Token,
                        RefreshTokenExpiration = refreshToken.Expiration,
                        CreatAt = DateTime.Now
                    };
                }

                return new UserToken()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expiration,
                    Message = "token de acesso gerado com sucesso.",
                    IsSuccess = true,
                    CreatAt = DateTime.Now
                };
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível gerar o token de acesso.");
            }
        }

        public async Task<IEnumerable<Claim>> GetUserRolesAsync(User user)
        {
            try
            {
                
                var roles = await _uof.UserRoleRepository.GetRolesByUserId(user.UserId);

                string[] names = user.Name.Split(' ');
                string firstName = names[0];
                string lastName = names.Length > 1 ? names[^1] : string.Empty;
                
                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Actor, $"{firstName} {lastName}"),
                    new Claim(ClaimTypes.Email, user.Login),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                };

                foreach (var item in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, item.Name));
                }

                return authClaims;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível extrair as roles do usuário");
            }
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expiration = DateTime.Now.AddDays(1),
                CreatAt = DateTime.Now
            };
        }

        public async Task<UserToken> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            if (principal == null)
            {
                throw new SecurityTokenException("Token inválido");
            }

            var refreshTokenDb = await _uof.RefreshTokenRepository.GetByTokenAsync(refreshToken);

            if (refreshTokenDb.Id == 0)
                throw new SecurityTokenException("Refresh token inválido");

            if (refreshTokenDb.Expiration <= DateTime.Now)
            {
                await _uof.RefreshTokenRepository.DeleteAsync(refreshTokenDb.Id);
                throw new SecurityTokenException("Refresh token expirado");
            }

            var newJwtToken = GenerateToken(principal.Claims, rememberMe: true);

            refreshTokenDb.Token = newJwtToken.RefreshToken;
            refreshTokenDb.Expiration = newJwtToken.RefreshTokenExpiration;
            refreshTokenDb.CreatAt = newJwtToken.CreatAt;

            await _uof.CommitAsync();

            return new UserToken
            {
                Token = newJwtToken.Token,
                Expiration = newJwtToken.Expiration,
                RefreshToken = newJwtToken.RefreshToken,
                RefreshTokenExpiration = newJwtToken.RefreshTokenExpiration,
                Message = "Token de acesso e refresh token gerados com sucesso.",
                IsSuccess = true,
                CreatAt = DateTime.Now
            };
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
                ValidateLifetime = false // Aqui estamos desativando a validação do tempo de vida do token
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Token inválido");
            }

            return principal;
        }
    }
}
