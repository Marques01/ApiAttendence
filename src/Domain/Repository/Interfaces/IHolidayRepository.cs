using Domain.Entities;

namespace Domain.Repository.Interfaces
{
    public interface IHolidayRepository
    {
        Task CreateAsync(Holiday holiday);

        Task<Holiday> GetHolidayByIdAsync(bool tracking, int id);

        Task<IEnumerable<Holiday>> GetAllHolidaysAsync(bool tracking);

        Task<IEnumerable<Holiday>> GetHolidaysByDateRangeAsync(bool tracking, DateTime startDate, DateTime endDate);
    }
}