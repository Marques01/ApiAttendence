using Application.Models.Response;

namespace Application.Services.Interfaces
{
    public interface IScheduleServices
    {
        Task<ScheduleResponseModel> GenerateSchedulesAsync(int classId, TimeOnly startTime, TimeOnly endTime);

        Task<ScheduleResponseModel> GetSchedulesByClassIdAsync(int classId);

        Task<ScheduleResponseModel> GetSchedulesByTeacherIdAsync(int teacherId);

        Task<ScheduleResponseModel> GetSchedulesByDateRangeAsync(DateTime startDate, DateTime endDate);

        Task<ScheduleResponseModel> GetSchedulesByDayOfWeekAsync(DayOfWeek dayOfWeek);

        Task<ScheduleResponseModel> GetActiveSchedulesAsync();

        Task<ScheduleResponseModel> GetHolidaySchedulesAsync();

        Task<int> GetTotalClassDaysAsync(int classId);

        Task<int> GetTotalHolidayDaysAsync(int classId);
    }
}