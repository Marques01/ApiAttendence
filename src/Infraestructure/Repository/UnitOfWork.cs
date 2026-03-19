using Domain.Repository.Interfaces;
using Infraestructure.Context;
using Microsoft.Extensions.Logging;

namespace Infraestructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public IStudentRepository StudentRepository { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            ILogger<UnitOfWork> logger,
            IStudentRepository studentRepository)
        {
            _context = context;
            _logger = logger;
            StudentRepository = studentRepository;
        }

        public async Task CommitAsync()
        {
            try
            {
                _logger.LogInformation("Committing changes to the database in UnitOfWork at {Date}", DateTime.UtcNow);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Changes committed successfully in UnitOfWork at {Date}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("An error occurred while committing changes in UnitOfWork at {Date}", DateTime.UtcNow);
                _logger.LogError(ex, "Exception details: {Message}", ex.Message);
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                _logger.LogInformation("Disposing UnitOfWork and its resources in {Date}", DateTime.UtcNow);
                _context.Dispose();
                _logger.LogInformation("UnitOfWork disposed successfully at {Date}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("An error occurred while disposing UnitOfWork at {Date}", DateTime.UtcNow);
                _logger.LogError(ex, "Exception details: {Message}", ex.Message);
                throw;
            }
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                _logger.LogInformation("Asynchronously disposing UnitOfWork and its resources in {Date}", DateTime.UtcNow);
                await _context.DisposeAsync();
                _logger.LogInformation("UnitOfWork asynchronously disposed successfully at {Date}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("An error occurred while asynchronously disposing UnitOfWork at {Date}", DateTime.UtcNow);
                _logger.LogError(ex, "Exception details: {Message}", ex.Message);
                throw;
            }
        }
    }
}
