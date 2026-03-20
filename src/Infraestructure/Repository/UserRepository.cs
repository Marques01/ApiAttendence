using Domain.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Infrastructure.Logger;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Não foi possível registrar o usuário\t";

                await RegisterLogs.CreateAsync(errorMessage += ex.Message, GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _context.Users
                    .Include(roles => roles.UserRoles)
                    .FirstOrDefaultAsync(x => x.UserId.Equals(id));

                return user ?? new User();
            }
            catch (Exception ex)
            {
                string errorMessage = $"Não foi possível buscar o usuário pelo id\t";

                await RegisterLogs.CreateAsync(errorMessage += ex.Message, GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<User> GetUserByLoginAsync(string mail)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Login.Equals(mail.Trim().ToLower()));

                if (user is not null)
                    return user;

                return new User();
            }
            catch (Exception ex)
            {
                string errorMessage = $"Não foi possível buscar o usuário pelo email\t";

                await RegisterLogs.CreateAsync(errorMessage += ex.Message, GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<User> SignInAsync(string login, string password)
        {
            try
            {
                var userResult = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Login.Equals(login.Trim().ToLower()));

                return userResult ?? new User();
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível verificar a existência do usuário\t";

                await RegisterLogs.CreateAsync(errorMessage += ex.Message, GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Não foi possível atualizar as informações do usuário\t";

                await RegisterLogs.CreateAsync(errorMessage += ex.Message, GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<bool> UserExistingAsync(string login)
        {
            try
            {
                var userExists = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Login.Equals(login));

                if (userExists is not null)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = $"Não foi possível verificar a existência do usuário\t";

                await RegisterLogs.CreateAsync(errorMessage += ex.Message, GetType().ToString());

                throw new Exception(errorMessage);
            }
        }
    }
}
