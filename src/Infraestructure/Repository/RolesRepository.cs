using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Infrastructure.Logger;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class RolesRepository : IRolesRepository
    {
        private readonly ApplicationDbContext _context;

        public RolesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Domain.Entities.Roles roles)
        {
            try
            {
                await _context.Roles.AddAsync(roles);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível criar a Role\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<IEnumerable<Domain.Entities.Roles>> GetRolesAsync()
        {
            return await _context.Roles.AsNoTracking().OrderBy(x => x.Name).ToListAsync();
        }
    }
}
