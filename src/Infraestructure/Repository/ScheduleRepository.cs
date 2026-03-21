using Domain.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ScheduleRepository> _logger;

        public ScheduleRepository(ApplicationDbContext context, ILogger<ScheduleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateAsync(Schedule schedule)
        {
            try
            {
                await _context.Schedules.AddAsync(schedule);
                _logger.LogInformation("Schedule created successfully for ClassId: {ClassId}, Date: {Date}", schedule.ClassId, schedule.Date);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating schedule for ClassId: {ClassId}", schedule.ClassId);
                throw;
            }
        }

        public async Task CreateMultipleAsync(IEnumerable<Schedule> schedules)
        {
            try
            {
                await _context.Schedules.AddRangeAsync(schedules);
                _logger.LogInformation("Multiple schedules created successfully. Count: {ScheduleCount}", schedules.Count());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating multiple schedules");
                throw;
            }
        }

        public async Task<Schedule> GetScheduleByIdAsync(bool tracking, int scheduleId)
        {
            try
            {
                return tracking
                    ? await _context.Schedules
                        .Include(s => s.Classes)
                        .Include(s => s.Teacher)
                        .FirstOrDefaultAsync(s => s.ScheduleId == scheduleId) ?? new()
                    : await _context.Schedules.AsNoTracking()
                        .Include(s => s.Classes)
                        .Include(s => s.Teacher)
                        .FirstOrDefaultAsync(s => s.ScheduleId == scheduleId) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving schedule with id: {ScheduleId}", scheduleId);
                throw;
            }
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByClassIdAsync(bool tracking, int classId)
        {
            try
            {
                return tracking
                    ? await _context.Schedules
                        .Include(s => s.Teacher)
                        .Where(s => s.ClassId == classId)
                        .OrderBy(s => s.Date)
                        .ToListAsync()
                    : await _context.Schedules.AsNoTracking()
                        .Include(s => s.Teacher)
                        .Where(s => s.ClassId == classId)
                        .OrderBy(s => s.Date)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving schedules for class id: {ClassId}", classId);
                throw;
            }
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByTeacherIdAsync(bool tracking, int teacherId)
        {
            try
            {
                return tracking
                    ? await _context.Schedules
                        .Include(s => s.Classes)
                        .Where(s => s.TeacherId == teacherId)
                        .OrderBy(s => s.Date)
                        .ToListAsync()
                    : await _context.Schedules.AsNoTracking()
                        .Include(s => s.Classes)
                        .Where(s => s.TeacherId == teacherId)
                        .OrderBy(s => s.Date)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving schedules for teacher id: {TeacherId}", teacherId);
                throw;
            }
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByDateRangeAsync(bool tracking, DateTime startDate, DateTime endDate)
        {
            try
            {
                return tracking
                    ? await _context.Schedules
                        .Include(s => s.Classes)
                        .Include(s => s.Teacher)
                        .Where(s => s.Date >= startDate && s.Date <= endDate)
                        .OrderBy(s => s.Date)
                        .ToListAsync()
                    : await _context.Schedules.AsNoTracking()
                        .Include(s => s.Classes)
                        .Include(s => s.Teacher)
                        .Where(s => s.Date >= startDate && s.Date <= endDate)
                        .OrderBy(s => s.Date)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving schedules for date range: {StartDate} - {EndDate}", startDate, endDate);
                throw;
            }
        }
    }
}