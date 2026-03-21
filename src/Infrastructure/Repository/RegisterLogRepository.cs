using Domain.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    public class RegisterLogRepository : IRegisterLogRepository
    {
        private readonly ApplicationDbContext _context;

        public RegisterLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(RegisterLog registerLog)
        {
            await _context.RegisterLogs.AddAsync(registerLog);
        }
    }
}
