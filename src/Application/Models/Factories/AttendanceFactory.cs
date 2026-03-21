using Application.Models.Request;
using Domain.Entities;

namespace Application.Models.Factories
{
    public static class AttendanceFactory
    {
        public static Attendance CreateAttendance(AttendanceRequestModel requestModel)
        {
            return new Attendance
            {
                ScheduleId = requestModel.ScheduleId,
                StudentId = requestModel.StudentId,
                Status = requestModel.Status,
                Notes = requestModel.Notes,
                Date = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue
            };
        }
    }
}