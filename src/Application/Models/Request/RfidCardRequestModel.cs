using Domain.Extensions;

namespace Application.Models.Request
{
    public class RfidCardRequestModel
    {
        private string _code = string.Empty;
        private int _number = 0;
        private bool _enabled = false;

        public string Code
        {
            get => _code;
            private set
            {
                string cleanInput = value.CleanInput();
                _code = cleanInput.ToUpper();
            }
        }

        public int Number
        {
            get => _number;
            private set => _number = value;
        }

        public bool Enabled
        {
            get => _enabled;
            private set => _enabled = value;
        }
    }
}