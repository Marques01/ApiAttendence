using Domain.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HolidayRepository> _logger;

        public HolidayRepository(ApplicationDbContext context, ILogger<HolidayRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateAsync(Holiday holiday)
        {
            try
            {
                await _context.Holidays.AddAsync(holiday);
                _logger.LogInformation("Holiday created successfully with name: {HolidayName}", holiday.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating holiday with name: {HolidayName}", holiday.Name);
                throw;
            }
        }

        public async Task<Holiday> GetHolidayByIdAsync(bool tracking, int id)
        {
            try
            {
                return tracking
                    ? await _context.Holidays.FirstOrDefaultAsync(h => h.Id == id) ?? new()
                    : await _context.Holidays.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving holiday with id: {HolidayId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<Holiday>> GetAllHolidaysAsync(bool tracking)
        {
            try
            {
                return tracking
                    ? await _context.Holidays.ToListAsync()
                    : await _context.Holidays.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all holidays");
                throw;
            }
        }

        public async Task<IEnumerable<Holiday>> GetHolidaysByDateRangeAsync(bool tracking, DateTime startDate, DateTime endDate)
        {
            try
            {
                return tracking
                    ? await _context.Holidays
                        .Where(h => h.Day >= startDate && h.Day <= endDate)
                        .ToListAsync()
                    : await _context.Holidays.AsNoTracking()
                        .Where(h => h.Day >= startDate && h.Day <= endDate)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving holidays for date range: {StartDate} - {EndDate}", startDate, endDate);
                throw;
            }
        }
    }
}