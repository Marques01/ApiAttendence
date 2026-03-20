using Domain.Extensions;

namespace Application.Models.Request
{
    public class HabilitationRequestModel
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
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

        public string Description
        {
            get => _description;
            init
            {
                string cleanInput = value.CleanInput();
                _description = cleanInput;
            }
        }

        public bool Enabled
        {
            get => _enabled;
            init => _enabled = value;
        }
    }
}