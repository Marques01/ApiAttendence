using Domain.Entities;

namespace Domain.Repository.Interfaces
{
    public interface IUserRolesRepository
    {
        Task<UserRoles> GetUserRolesByIdAsync(int id);

        Task CreateAsync(UserRoles userRoles);

        Task DeleteAsync(int id);

        Task<List<Entities.Roles>> GetRolesByUserId(int id);
    }
}
