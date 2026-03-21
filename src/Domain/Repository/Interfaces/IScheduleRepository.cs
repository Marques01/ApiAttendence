using Domain.Entities;

namespace Domain.Repository.Interfaces
{
    public interface IScheduleRepository
    {
        Task CreateAsync(Schedule schedule);

        Task CreateMultipleAsync(IEnumerable<Schedule> schedules);

        Task<Schedule> GetScheduleByIdAsync(bool tracking, int scheduleId);

        Task<IEnumerable<Schedule>> GetSchedulesByClassIdAsync(bool tracking, int classId);

        Task<IEnumerable<Schedule>> GetSchedulesByTeacherIdAsync(bool tracking, int teacherId);

        Task<IEnumerable<Schedule>> GetSchedulesByDateRangeAsync(bool tracking, DateTime startDate, DateTime endDate);
    }
}