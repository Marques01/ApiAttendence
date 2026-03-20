namespace Domain.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        public IStudentRepository StudentRepository { get; }
        public IRfidCardRepository RfidCardRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserRolesRepository UserRoleRepository { get; }
        public IRolesRepository RoleRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        public IRegisterLogRepository RegisterLogRepository { get; }
        public ITeacherRepository TeacherRepository { get; }
        public IHabilitationRepository HabilitationRepository { get; }
        public ITeacherHabilitationRepository TeacherHabilitationRepository { get; }
        Task CommitAsync();
    }
}
