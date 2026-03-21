using Domain.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RefreshTokenRepository> _logger;

        public RefreshTokenRepository(ApplicationDbContext context, ILogger<RefreshTokenRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateAsync(RefreshToken refreshToken)
        {
            try
            {
                await _context.RefreshTokens.AddAsync(refreshToken);
                _logger.LogInformation("Refresh token created successfully at {Date}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating refresh token at {Date}", DateTime.UtcNow);
                throw;
            }
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            try
            {
                return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token) ?? new RefreshToken();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving refresh token at {Date}", DateTime.UtcNow);
                throw;
            }
        }

        public async Task UpdateAsync(RefreshToken refreshToken)
        {
            try
            {
                _context.RefreshTokens.Update(refreshToken);
                _logger.LogInformation("Refresh token updated successfully at {Date}", DateTime.UtcNow);
                await Task.FromResult(Task.CompletedTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating refresh token at {Date}", DateTime.UtcNow);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Id == id);

                if (refreshToken is null)
                    throw new ArgumentException("Token de atualização não encontrado.");

                _context.RefreshTokens.Remove(refreshToken);
            }
            catch (Exception ex)
            {   
                _logger.LogError(ex, "Error deleting refresh token at {Date}", DateTime.UtcNow);
                throw;
            }
        }
    }
}
