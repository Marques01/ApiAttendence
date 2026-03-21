using Domain.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class ClassesRepository : IClassesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClassesRepository> _logger;

        public ClassesRepository(ApplicationDbContext context, ILogger<ClassesRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateAsync(Classes classes)
        {
            try
            {
                await _context.Classes.AddAsync(classes);
                _logger.LogInformation("Classes created successfully with name: {ClassName}", classes.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating classes with name: {ClassName}", classes.Name);
                throw;
            }
        }

        public async Task<Classes> GetClassesByIdAsync(bool tracking, int classId)
        {
            try
            {
                return tracking
                    ? await _context.Classes
                        .Include(c => c.Teacher)
                        .Include(c => c.Classroom)
                        .FirstOrDefaultAsync(c => c.ClassId == classId) ?? new()
                    : await _context.Classes.AsNoTracking()
                        .Include(c => c.Teacher)
                        .Include(c => c.Classroom)
                        .FirstOrDefaultAsync(c => c.ClassId == classId) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving classes with id: {ClassId}", classId);
                throw;
            }
        }

        public async Task<IEnumerable<Classes>> GetClassesByTeacherIdAsync(bool tracking, int teacherId)
        {
            try
            {
                return tracking
                    ? await _context.Classes
                        .Include(c => c.Classroom)
                        .Where(c => c.TeacherId == teacherId)
                        .ToListAsync()
                    : await _context.Classes.AsNoTracking()
                        .Include(c => c.Classroom)
                        .Where(c => c.TeacherId == teacherId)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving classes for teacher id: {TeacherId}", teacherId);
                throw;
            }
        }

        public async Task<IEnumerable<Classes>> GetAllClassesAsync(bool tracking)
        {
            try
            {
                return tracking
                    ? await _context.Classes
                        .Include(c => c.Teacher)
                        .Include(c => c.Classroom)
                        .ToListAsync()
                    : await _context.Classes.AsNoTracking()
                        .Include(c => c.Teacher)
                        .Include(c => c.Classroom)
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all classes");
                throw;
            }
        }
    }
}