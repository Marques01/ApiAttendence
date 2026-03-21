using Application.Models.Request;
using Application.Models.Response;

namespace Application.Services.Interfaces
{
    public interface IClassesServices
    {
        Task<ClassesResponseModel> CreateAsync(ClassesRequestModel requestModel);
    }
}