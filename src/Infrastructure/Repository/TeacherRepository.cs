using Domain.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TeacherRepository> _logger;

        public TeacherRepository(ApplicationDbContext context, ILogger<TeacherRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateAsync(Teacher teacher)
        {
            try
            {
                await _context.Teachers.AddAsync(teacher);
                _logger.LogInformation("Teacher created successfully with registration: {Registration}", teacher.Registration);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating teacher with registration: {Registration}", teacher.Registration);
                throw;
            }
        }

        public async Task<Teacher> GetTeacherByRegistrationAsync(bool tracking, string registration)
        {
            try
            {
                return tracking
                    ? await _context.Teachers.FirstOrDefaultAsync(t => t.Registration == registration) ?? new()
                    : await _context.Teachers.AsNoTracking().FirstOrDefaultAsync(t => t.Registration == registration) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving teacher with registration: {Registration}", registration);
                throw;
            }
        }

        public async Task<Teacher> GetTeacherByEmailAsync(bool tracking, string email)
        {
            try
            {
                return tracking
                    ? await _context.Teachers.FirstOrDefaultAsync(t => t.Email == email) ?? new()
                    : await _context.Teachers.AsNoTracking().FirstOrDefaultAsync(t => t.Email == email) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving teacher with email: {Email}", email);
                throw;
            }
        }

        public async Task<Teacher> GetTeacherByIdAsync(bool tracking, int teacherId)
        {
            try
            {
                return tracking
                    ? await _context.Teachers.FirstOrDefaultAsync(t => t.TeacherId == teacherId) ?? new()
                    : await _context.Teachers.AsNoTracking().FirstOrDefaultAsync(t => t.TeacherId == teacherId) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving teacher with id: {TeacherId}", teacherId);
                throw;
            }
        }
    }
}