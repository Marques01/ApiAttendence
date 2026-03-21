using Application.Models.Request;
using Domain.Entities;
using Domain.Extensions;

namespace Application.Models.Factories
{
    public static class HolidayFactory
    {
        public static Holiday CreateHoliday(HolidayRequestModel requestModel)
        {
            return new Holiday
            {
                Name = requestModel.Name.CapitalizeFirstLetters().Trim(),
                Day = requestModel.Day,
                IsFixedDate = requestModel.IsFixedDate,
                National = requestModel.National,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue
            };
        }
    }
}