using Application.Models.Request;
using Application.Models.Response;

namespace Application.Services.Interfaces
{
    public interface ITeacherServices
    {
        Task<TeacherResponseModel> CreateAsync(TeacherRequestModel requestModel);
    }
}