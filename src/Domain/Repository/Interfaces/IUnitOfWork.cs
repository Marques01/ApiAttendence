namespace Domain.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        public IStudentRepository StudentRepository { get; }

        Task CommitAsync();
    }
}
