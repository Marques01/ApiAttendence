using Application.Models.Request;
using Application.Models.Response;

namespace Application.Services.Interfaces
{
    public interface IHabilitationServices
    {
        Task<HabilitationResponseModel> CreateAsync(HabilitationRequestModel requestModel);
    }
}