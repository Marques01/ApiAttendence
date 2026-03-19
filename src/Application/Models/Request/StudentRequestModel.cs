using Domain.Extensions;

namespace Application.Models.Request
{
    public class StudentRequestModel
    {
        private string
            _name = string.Empty;

        private string
            _registration = string.Empty;

        private bool
            _enabled = false;

        private int
            _rfidCardId = 0;

        public string Name
        {
            get => _name;
            private set
            {
                string cleanInput = value.CleanInput();
                _name = cleanInput.CapitalizeFirstLetters();
            }
        }

        public string Registration
        {
            get => _registration;
            private set
            {
                string cleanInput = value.CleanInput();
                _registration = cleanInput.ToUpper();
            }
        }

        public bool Enabled
        {
            get => _enabled;
            private set => _enabled = value;
        }

        public int RfidCardId
        {
            get => _rfidCardId;
            private set => _rfidCardId = value;
        }
    }
}
