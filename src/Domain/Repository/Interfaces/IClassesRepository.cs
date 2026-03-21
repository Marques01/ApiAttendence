using Domain.Entities;

namespace Domain.Repository.Interfaces
{
    public interface IClassesRepository
    {
        Task CreateAsync(Classes classes);

        Task<Classes> GetClassesByIdAsync(bool tracking, int classId);

        Task<IEnumerable<Classes>> GetClassesByTeacherIdAsync(bool tracking, int teacherId);

        Task<IEnumerable<Classes>> GetAllClassesAsync(bool tracking);
    }
}