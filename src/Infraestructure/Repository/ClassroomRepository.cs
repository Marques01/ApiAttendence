using Domain.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClassroomRepository> _logger;

        public ClassroomRepository(ApplicationDbContext context, ILogger<ClassroomRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateAsync(Classroom classroom)
        {
            try
            {
                await _context.Classrooms.AddAsync(classroom);
                _logger.LogInformation("Classroom created successfully with name: {ClassroomName}", classroom.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating classroom with name: {ClassroomName}", classroom.Name);
                throw;
            }
        }

        public async Task<Classroom> GetClassroomByIdAsync(bool tracking, int classroomId)
        {
            try
            {
                return tracking
                    ? await _context.Classrooms.FirstOrDefaultAsync(c => c.Id == classroomId) ?? new()
                    : await _context.Classrooms.AsNoTracking().FirstOrDefaultAsync(c => c.Id == classroomId) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving classroom with id: {ClassroomId}", classroomId);
                throw;
            }
        }

        public async Task<Classroom> GetClassroomByNameAsync(bool tracking, string name)
        {
            try
            {
                return tracking
                    ? await _context.Classrooms.FirstOrDefaultAsync(c => c.Name == name) ?? new()
                    : await _context.Classrooms.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving classroom with name: {ClassroomName}", name);
                throw;
            }
        }

        public async Task<IEnumerable<Classroom>> GetAllClassroomsAsync(bool tracking)
        {
            try
            {
                return tracking
                    ? await _context.Classrooms.ToListAsync()
                    : await _context.Classrooms.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all classrooms");
                throw;
            }
        }

        public async Task UpdateAsync(Classroom classroom)
        {
            try
            {
                _context.Classrooms.Update(classroom);
                _logger.LogInformation("Classroom updated successfully with id: {ClassroomId}", classroom.Id);
                await Task.CompletedTask; // simulate async operation
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating classroom with id: {ClassroomId}", classroom.Id);
                throw;
            }
        }
    }
}