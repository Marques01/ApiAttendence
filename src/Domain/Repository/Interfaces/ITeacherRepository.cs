using Domain.Entities;

namespace Domain.Repository.Interfaces
{
    public interface ITeacherRepository
    {
        Task CreateAsync(Teacher teacher);

        Task<Teacher> GetTeacherByRegistrationAsync(bool tracking, string registration);

        Task<Teacher> GetTeacherByEmailAsync(bool tracking, string email);

        Task<Teacher> GetTeacherByIdAsync(bool tracking, int teacherId);
    }
}