using Application.Models.Request;
using Application.Models.Response;

namespace Application.Services.Interfaces
{
    public interface IScheduleServices
    {
        Task<ScheduleResponseModel> GenerateSchedulesAsync(int classId, TimeOnly startTime, TimeOnly endTime);

        Task<List<ScheduleResponseModel>> GetSchedulesByClassIdAsync(int classId);

        Task<List<ScheduleResponseModel>> GetSchedulesByTeacherIdAsync(int teacherId);

        Task<List<ScheduleResponseModel>> GetSchedulesByDateRangeAsync(DateTime startDate, DateTime endDate);

        Task<List<ScheduleResponseModel>> GetSchedulesByDayOfWeekAsync(DayOfWeek dayOfWeek);

        Task<List<ScheduleResponseModel>> GetActiveSchedulesAsync();

        Task<List<ScheduleResponseModel>> GetHolidaySchedulesAsync();

        Task<int> GetTotalClassDaysAsync(int classId);

        Task<int> GetTotalHolidayDaysAsync(int classId);
    }
}