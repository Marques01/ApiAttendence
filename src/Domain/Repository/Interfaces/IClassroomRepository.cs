using Domain.Entities;

namespace Domain.Repository.Interfaces
{
    public interface IClassroomRepository
    {
        Task CreateAsync(Classroom classroom);

        Task<Classroom> GetClassroomByIdAsync(bool tracking, int classroomId);

        Task<Classroom> GetClassroomByNameAsync(bool tracking, string name);

        Task<IEnumerable<Classroom>> GetAllClassroomsAsync(bool tracking);

        Task UpdateAsync(Classroom classroom);
    }
}