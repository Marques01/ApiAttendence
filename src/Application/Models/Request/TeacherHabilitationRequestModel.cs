namespace Application.Models.Request
{
    public class TeacherHabilitationRequestModel
    {
        private int _teacherId = 0;
        private int _habilitationId = 0;
        private bool _enabled = false;

        public int TeacherId
        {
            get => _teacherId;
            init => _teacherId = value;
        }

        public int HabilitationId
        {
            get => _habilitationId;
            init => _habilitationId = value;
        }

        public bool Enabled
        {
            get => _enabled;
            init => _enabled = value;
        }
    }
}