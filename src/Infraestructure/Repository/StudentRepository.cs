using Domain.Entities;
using Domain.Repository.Interfaces;
using Infraestructure.Context;

namespace Infraestructure.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Student student)
        {
            await _context.Students.AddAsync(student);
        }
    }
}
