using Domain.Extensions;

namespace Application.Models.Request
{
    public class TeacherRequestModel
    {
        private string _name = string.Empty;
        private string _registration = string.Empty;
        private string _email = string.Empty;
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

        public string Registration
        {
            get => _registration;
            init
            {
                string cleanInput = value.CleanInput();
                _registration = cleanInput.ToUpper();
            }
        }

        public string Email
        {
            get => _email;
            init
            {
                string cleanInput = value.CleanInput().ToLower().Trim();
                _email = cleanInput;
            }
        }

        public bool Enabled
        {
            get => _enabled;
            init => _enabled = value;
        }
    }
}