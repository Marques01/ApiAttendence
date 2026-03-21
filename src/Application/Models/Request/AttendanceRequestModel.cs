namespace Application.Models.Request
{
    public class AttendanceRequestModel
    {
        private int _scheduleId = 0;
        private int _studentId = 0;
        private string _status = string.Empty;
        private string _notes = string.Empty;

        public int ScheduleId
        {
            get => _scheduleId;
            init => _scheduleId = value;
        }

        public int StudentId
        {
            get => _studentId;
            init => _studentId = value;
        }

        public string Status
        {
            get => _status;
            init => _status = value;
        }

        public string Notes
        {
            get => _notes;
            init => _notes = value;
        }
    }
}