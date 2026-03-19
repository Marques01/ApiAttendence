using Domain.Entities;

namespace Domain.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Task CreateAsync(Student student);
    }
}
