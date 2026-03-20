using Application.Models.Request;
using Application.Models.Response;

namespace Application.Services.Interfaces
{
    public interface IRfidCardServices
    {
        Task<RfidCardResponseModel> CreateAsync(RfidCardRequestModel requestModel);
    }
}