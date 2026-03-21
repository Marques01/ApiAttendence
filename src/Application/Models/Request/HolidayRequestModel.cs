using Domain.Extensions;

namespace Application.Models.Request
{
    public class HolidayRequestModel
    {
        private string _name = string.Empty;
        private bool _isFixedDate = false;
        private bool _national = false;

        public string Name
        {
            get => _name;
            init
            {
                string cleanInput = value.CleanInput();
                _name = cleanInput.CapitalizeFirstLetters();
            }
        }

        public DateTime Day { get; init; }

        public bool IsFixedDate
        {
            get => _isFixedDate;
            init => _isFixedDate = value;
        }

        public bool National
        {
            get => _national;
            init => _national = value;
        }
    }
}