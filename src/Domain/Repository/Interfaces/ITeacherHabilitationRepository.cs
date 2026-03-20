using Domain.Entities;

namespace Domain.Repository.Interfaces
{
    public interface ITeacherHabilitationRepository
    {
        Task CreateAsync(TeacherHabilitation teacherHabilitation);

        Task<TeacherHabilitation> GetByIdAsync(bool tracking, int teacherHabilitationId);

        Task<IEnumerable<TeacherHabilitation>> GetTeacherHabilitationsAsync(bool tracking, int teacherId);

        Task<TeacherHabilitation> GetByTeacherAndHabilitationAsync(bool tracking, int teacherId, int habilitationId);

        Task DeleteAsync(int teacherHabilitationId);
    }
}