using Domain.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AttendanceRepository> _logger;

        public AttendanceRepository(ApplicationDbContext context, ILogger<AttendanceRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateAsync(Attendance attendance)
        {
            try
            {
                await _context.Attendances.AddAsync(attendance);
                _logger.LogInformation("Attendance created successfully for ScheduleId: {ScheduleId}, StudentId: {StudentId}", attendance.ScheduleId, attendance.StudentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating attendance for ScheduleId: {ScheduleId}", attendance.ScheduleId);
                throw;
            }
        }

        public async Task<Attendance> GetAttendanceByIdAsync(bool tracking, int id)
        {
            try
            {
                return tracking
                    ? await _context.Attendances
                        .Include(a => a.Schedule)
                        .Include(a => a.Student)
                        .FirstOrDefaultAsync(a => a.Id == id) ?? new()
                    : await _context.Attendances.AsNoTracking()
                        .Include(a => a.Schedule)
                        .Include(a => a.Student)
                        .FirstOrDefaultAsync(a => a.Id == id) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving attendance with id: {AttendanceId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<Attendance>> GetAttendancesByScheduleIdAsync(bool tracking, int scheduleId)
        {
            try
            {
                return tracking
                    ? await _context.Attendances
                        .Include(a => a.Student)
                        .Where(a => a.ScheduleId == scheduleId)
                        .ToListAsync()
                    : await _context.Attendances.AsNoTracking()
                        .Include(a => a.Student)
                        .Where(a => a.ScheduleId == scheduleId)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving attendances for schedule id: {ScheduleId}", scheduleId);
                throw;
            }
        }

        public async Task<IEnumerable<Attendance>> GetAttendancesByStudentIdAsync(bool tracking, int studentId)
        {
            try
            {
                return tracking
                    ? await _context.Attendances
                        .Include(a => a.Schedule)
                        .Where(a => a.StudentId == studentId)
                        .ToListAsync()
                    : await _context.Attendances.AsNoTracking()
                        .Include(a => a.Schedule)
                        .Where(a => a.StudentId == studentId)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving attendances for student id: {StudentId}", studentId);
                throw;
            }
        }

        public async Task<IEnumerable<Attendance>> GetAttendancesByDateRangeAsync(bool tracking, DateTime startDate, DateTime endDate)
        {
            try
            {
                return tracking
                    ? await _context.Attendances
                        .Include(a => a.Schedule)
                        .Include(a => a.Student)
                        .Where(a => a.Date >= startDate && a.Date <= endDate)
                        .ToListAsync()
                    : await _context.Attendances.AsNoTracking()
                        .Include(a => a.Schedule)
                        .Include(a => a.Student)
                        .Where(a => a.Date >= startDate && a.Date <= endDate)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving attendances for date range: {StartDate} - {EndDate}", startDate, endDate);
                throw;
            }
        }

        public async Task UpdateAsync(Attendance attendance)
        {
            try
            {
                _context.Attendances.Update(attendance);
                _logger.LogInformation("Attendance updated successfully with id: {AttendanceId}", attendance.Id);
                await Task.CompletedTask; // Simulate async operation for consistency
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating attendance with id: {AttendanceId}", attendance.Id);
                throw;
            }
        }
    }
}