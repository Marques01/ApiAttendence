using Domain.Entities;

namespace Domain.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Task CreateAsync(Student student);

        Task<Student> GetStudentByRegistrationAsync(bool tracking, string code);
    }
}
