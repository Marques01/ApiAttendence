using Domain.Entities;

namespace Domain.Repository.Interfaces
{
    public interface IRegisterLogRepository
    {
        Task CreateAsync(RegisterLog registerLog);
    }
}
