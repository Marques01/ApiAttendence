using Domain.Entities;

namespace Domain.Repository.Interfaces
{
    public interface IAttendanceRepository
    {
        Task CreateAsync(Attendance attendance);

        Task<Attendance> GetAttendanceByIdAsync(bool tracking, int id);

        Task<IEnumerable<Attendance>> GetAttendancesByScheduleIdAsync(bool tracking, int scheduleId);

        Task<IEnumerable<Attendance>> GetAttendancesByStudentIdAsync(bool tracking, int studentId);

        Task<IEnumerable<Attendance>> GetAttendancesByDateRangeAsync(bool tracking, DateTime startDate, DateTime endDate);

        Task UpdateAsync(Attendance attendance);
    }
}