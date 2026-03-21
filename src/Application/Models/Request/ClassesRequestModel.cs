using Domain.Extensions;

namespace Application.Models.Request
{
    public class ClassesRequestModel
    {
        private string _name = string.Empty;
        private int _teacherId = 0;
        private int _classroomId = 0;
        private string _notes = string.Empty;
        private bool _enabled = false;

        public string Name
        {
            get => _name;
            init
            {
                string cleanInput = value.CleanInput();
                _name = cleanInput.CapitalizeFirstLetters();
            }
        }

        public int TeacherId
        {
            get => _teacherId;
            init => _teacherId = value;
        }

        public int ClassroomId
        {
            get => _classroomId;
            init => _classroomId = value;
        }

        public DateTime StartDate { get; init; }

        public DateTime EndDate { get; init; }

        public string Notes
        {
            get => _notes;
            init
            {
                string cleanInput = value.CleanInput();
                _notes = cleanInput;
            }
        }

        public bool Enabled
        {
            get => _enabled;
            init => _enabled = value;
        }
    }
}