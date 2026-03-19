using Application.Models.Request;
using Application.Models.Response;

namespace Application.Services.Interfaces
{
    public interface IStudentServices
    {
        Task<StudentResponseModel> CreateAsync(StudentRequestModel requestModel);
    }
}
