using Domain.Entities;

namespace Application.Models.Factories
{
    public static class ScheduleFactory
    {
        public static Schedule CreateSchedule(int classId, int teacherId, DateTime date, TimeOnly startTime, TimeOnly endTime, bool isHoliday = false)
        {
            return new Schedule
            {
                ClassId = classId,
                TeacherId = teacherId,
                Date = date,
                DayOfWeek = date.DayOfWeek,
                StartTime = startTime,
                EndTime = endTime,
                IsHoliday = isHoliday,
                Enabled = !isHoliday,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
                DisabledAt = isHoliday ? DateTime.Now : DateTime.MinValue
            };
        }
    }
}