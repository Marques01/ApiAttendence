using Domain.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class RfidCardRepository : IRfidCardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RfidCardRepository> _logger;

        public RfidCardRepository(ApplicationDbContext context, ILogger<RfidCardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateAsync(RfidCard rfidCard)
        {
            try
            {
                await _context.RfidCards.AddAsync(rfidCard);
                _logger.LogInformation("RFID Card created successfully with code: {Code}", rfidCard.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating RFID Card with code: {Code}", rfidCard.Code);
                throw;
            }
        }
    }
}