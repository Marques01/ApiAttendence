using Application.Models.Request;
using Application.Models.Response;

namespace Application.Services.Interfaces
{
    public interface ITeacherHabilitationServices
    {
        Task<TeacherHabilitationResponseModel> CreateAsync(TeacherHabilitationRequestModel requestModel);

        Task<TeacherHabilitationResponseModel> DeleteAsync(int teacherHabilitationId);

        Task<List<TeacherHabilitationResponseModel>> GetTeacherHabilitationsAsync(int teacherId);
    }
}