using Domain.Entities;

namespace Domain.Repository.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task CreateAsync(RefreshToken refreshToken);

        Task<RefreshToken> GetByTokenAsync(string token);

        Task UpdateAsync(RefreshToken refreshToken);

        Task DeleteAsync(int id);
    }
}
