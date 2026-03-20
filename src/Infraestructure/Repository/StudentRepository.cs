using Domain.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StudentRepository> _logger;

        public StudentRepository(ApplicationDbContext context, ILogger<StudentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateAsync(Student student)
        {
            try
            {
                await _context.Students.AddAsync(student);
                _logger.LogInformation("Student created successfully with registration: {Registration}", student.Registration);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating student with registration: {Registration}", student.Registration);
                throw;
            }
        }

        public async Task<Student> GetStudentByRegistrationAsync(bool tracking, string code)
        {
            try
            {
                return tracking
                    ? await _context.Students.FirstOrDefaultAsync(s => s.Registration == code) ?? new()
                    : await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Registration == code) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving student with registration: {Registration}", code);
                throw;
            }
        }
    }
}
