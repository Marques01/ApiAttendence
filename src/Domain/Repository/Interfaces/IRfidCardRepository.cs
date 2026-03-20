using Domain.Entities;

namespace Domain.Repository.Interfaces
{
    public interface IRfidCardRepository
    {
        Task CreateAsync(RfidCard rfidCard);
    }
}