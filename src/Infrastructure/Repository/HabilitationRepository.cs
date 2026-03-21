using Domain.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class HabilitationRepository : IHabilitationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HabilitationRepository> _logger;

        public HabilitationRepository(ApplicationDbContext context, ILogger<HabilitationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateAsync(Habilitation habilitation)
        {
            try
            {
                await _context.Habilitations.AddAsync(habilitation);
                _logger.LogInformation("Habilitation created successfully with name: {Name}", habilitation.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating habilitation with name: {Name}", habilitation.Name);
                throw;
            }
        }

        public async Task<Habilitation> GetHabilitationByNameAsync(bool tracking, string name)
        {
            try
            {
                return tracking
                    ? await _context.Habilitations.FirstOrDefaultAsync(h => h.Name == name) ?? new()
                    : await _context.Habilitations.AsNoTracking().FirstOrDefaultAsync(h => h.Name == name) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving habilitation with name: {Name}", name);
                throw;
            }
        }

        public async Task<Habilitation> GetHabilitationByIdAsync(bool tracking, int id)
        {
            try
            {
                return tracking
                    ? await _context.Habilitations.FirstOrDefaultAsync(h => h.HabilitationId == id) ?? new()
                    : await _context.Habilitations.AsNoTracking().FirstOrDefaultAsync(h => h.HabilitationId == id) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving habilitation with id: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<Habilitation>> GetAllHabilitationsAsync(bool tracking)
        {
            try
            {
                return tracking
                    ? await _context.Habilitations.ToListAsync()
                    : await _context.Habilitations.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all habilitations");
                throw;
            }
        }
    }
}