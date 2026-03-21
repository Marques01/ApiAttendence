using Application.Models.Request;
using Application.Models.Response;

namespace Application.Services.Interfaces
{
    public interface IHolidayServices
    {
        Task<HolidayResponseModel> CreateAsync(HolidayRequestModel requestModel);
    }
}