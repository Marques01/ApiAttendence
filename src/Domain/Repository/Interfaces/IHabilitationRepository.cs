using Domain.Entities;

namespace Domain.Repository.Interfaces
{
    public interface IHabilitationRepository
    {
        Task CreateAsync(Habilitation habilitation);

        Task<Habilitation> GetHabilitationByNameAsync(bool tracking, string name);

        Task<Habilitation> GetHabilitationByIdAsync(bool tracking, int id);

        Task<IEnumerable<Habilitation>> GetAllHabilitationsAsync(bool tracking);
    }
}