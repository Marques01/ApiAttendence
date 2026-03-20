namespace Domain.Repository.Interfaces
{
    public interface IRolesRepository
    {
        Task CreateAsync(Entities.Roles roles);

        Task<IEnumerable<Entities.Roles>> GetRolesAsync();
    }
}
