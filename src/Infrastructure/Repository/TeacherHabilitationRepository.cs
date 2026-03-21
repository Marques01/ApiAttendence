using Domain.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class TeacherHabilitationRepository : ITeacherHabilitationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TeacherHabilitationRepository> _logger;

        public TeacherHabilitationRepository(ApplicationDbContext context, ILogger<TeacherHabilitationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateAsync(TeacherHabilitation teacherHabilitation)
        {
            try
            {
                await _context.TeacherHabilitations.AddAsync(teacherHabilitation);
                _logger.LogInformation("TeacherHabilitation created successfully for TeacherId: {TeacherId}, HabilitationId: {HabilitationId}",
                    teacherHabilitation.TeacherId, teacherHabilitation.HabilitationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating TeacherHabilitation for TeacherId: {TeacherId}", teacherHabilitation.TeacherId);
                throw;
            }
        }

        public async Task<TeacherHabilitation> GetByIdAsync(bool tracking, int teacherHabilitationId)
        {
            try
            {
                return tracking
                    ? await _context.TeacherHabilitations
                        .Include(th => th.Teacher)
                        .Include(th => th.Habilitation)
                        .FirstOrDefaultAsync(th => th.TeacherHabilitationId == teacherHabilitationId) ?? new()
                    : await _context.TeacherHabilitations.AsNoTracking()
                        .Include(th => th.Teacher)
                        .Include(th => th.Habilitation)
                        .FirstOrDefaultAsync(th => th.TeacherHabilitationId == teacherHabilitationId) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving TeacherHabilitation with id: {Id}", teacherHabilitationId);
                throw;
            }
        }

        public async Task<IEnumerable<TeacherHabilitation>> GetTeacherHabilitationsAsync(bool tracking, int teacherId)
        {
            try
            {
                return tracking
                    ? await _context.TeacherHabilitations
                        .Include(th => th.Habilitation)
                        .Where(th => th.TeacherId == teacherId)
                        .ToListAsync()
                    : await _context.TeacherHabilitations.AsNoTracking()
                        .Include(th => th.Habilitation)
                        .Where(th => th.TeacherId == teacherId)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving habilitaitons for TeacherId: {TeacherId}", teacherId);
                throw;
            }
        }

        public async Task<TeacherHabilitation> GetByTeacherAndHabilitationAsync(bool tracking, int teacherId, int habilitationId)
        {
            try
            {
                return tracking
                    ? await _context.TeacherHabilitations
                        .Include(th => th.Teacher)
                        .Include(th => th.Habilitation)
                        .FirstOrDefaultAsync(th => th.TeacherId == teacherId && th.HabilitationId == habilitationId) ?? new()
                    : await _context.TeacherHabilitations.AsNoTracking()
                        .Include(th => th.Teacher)
                        .Include(th => th.Habilitation)
                        .FirstOrDefaultAsync(th => th.TeacherId == teacherId && th.HabilitationId == habilitationId) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving TeacherHabilitation for TeacherId: {TeacherId}, HabilitationId: {HabilitationId}", teacherId, habilitationId);
                throw;
            }
        }

        public async Task DeleteAsync(int teacherHabilitationId)
        {
            try
            {
                var teacherHabilitation = await GetByIdAsync(true, teacherHabilitationId);

                if (teacherHabilitation.TeacherHabilitationId == 0)
                {
                    _logger.LogWarning("TeacherHabilitation not found with id: {Id}", teacherHabilitationId);
                    throw new ArgumentException($"TeacherHabilitation not found with id: {teacherHabilitationId}");
                }

                _context.TeacherHabilitations.Remove(teacherHabilitation);
                _logger.LogInformation("TeacherHabilitation deleted successfully for TeacherId: {TeacherId}, HabilitationId: {HabilitationId}", 
                    teacherHabilitation.TeacherId, teacherHabilitation.HabilitationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting TeacherHabilitation with id: {Id}", teacherHabilitationId);
                throw;
            }
        }
    }
}