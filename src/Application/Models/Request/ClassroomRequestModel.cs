using Domain.Extensions;

namespace Application.Models.Request
{
    public class ClassroomRequestModel
    {
        private string _name = string.Empty;
        private string _location = string.Empty;
        private int _capacity = 0;
        private bool _isAvailable = false;

        public string Name
        {
            get => _name;
            init
            {
                string cleanInput = value.CleanInput();
                _name = cleanInput.CapitalizeFirstLetters();
            }
        }

        public int Capacity
        {
            get => _capacity;
            init => _capacity = value;
        }

        public string Location
        {
            get => _location;
            init
            {
                string cleanInput = value.CleanInput();
                _location = cleanInput;
            }
        }

        public bool IsAvailable
        {
            get => _isAvailable;
            init => _isAvailable = value;
        }
    }
}