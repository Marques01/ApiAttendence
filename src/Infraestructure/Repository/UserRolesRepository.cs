using Domain.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Infrastructure.Logger;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRolesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(UserRoles userRoles)
        {
            try
            {
                await _context.UserRoles.AddAsync(userRoles);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível associar o usuário a role\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var userRoles = await GetUserRolesByIdAsync(id);

                _context.UserRoles.Remove(userRoles);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível remover a associação do usuário a role\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<List<Roles>> GetRolesByUserId(int id)
        {
            try
            {
                var userRoles = await _context.UserRoles
                    .AsNoTracking()
                    .Where(x => x.UserId.Equals(id))
                    .ToListAsync();

                List<Roles> roles = new();

                foreach (var item in userRoles)
                {
                    var role = await _context.Roles.
                        AsNoTracking()
                        .FirstOrDefaultAsync(x => x.RoleId.Equals(item.RoleId));

                    if (role is not null)
                        roles.Add(role);
                }

                return roles;
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível buscar a associação do usuário a role\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<UserRoles> GetUserRolesByIdAsync(int id)
        {
            try
            {
                var userRoles = await _context.UserRoles.FirstOrDefaultAsync(x => x.UserRolesId.Equals(id));

                if (userRoles is not null)
                    return userRoles;

                return new UserRoles();
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível buscar a associação pelo id\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", GetType().ToString());

                throw new Exception(errorMessage);
            }
        }
    }
}
