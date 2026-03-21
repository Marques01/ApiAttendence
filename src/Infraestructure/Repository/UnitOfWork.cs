using Domain.Repository.Interfaces;
using Infrastructure.Context;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

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
        public IClassesRepository ClassesRepository { get; }
        public IClassroomRepository ClassroomRepository { get; }
        public IHolidayRepository HolidayRepository { get; }
        public IScheduleRepository ScheduleRepository { get; }
        public IAttendanceRepository AttendanceRepository { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            ILogger<UnitOfWork> logger,
            IStudentRepository studentRepository,
            IRfidCardRepository rfidCardRepository,
            IUserRepository userRepository,
            IUserRolesRepository userRoleRepository,
            IRolesRepository roleRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IRegisterLogRepository registerLogRepository,
            ITeacherRepository teacherRepository,
            IHabilitationRepository habilitationRepository,
            ITeacherHabilitationRepository teacherHabilitationRepository,
            IClassesRepository classesRepository,
            IClassroomRepository classroomRepository,
            IHolidayRepository holidayRepository,
            IScheduleRepository scheduleRepository,
            IAttendanceRepository attendanceRepository)
        {
            _context = context;
            _logger = logger;
            StudentRepository = studentRepository;
            RfidCardRepository = rfidCardRepository;
            UserRepository = userRepository;
            UserRoleRepository = userRoleRepository;
            RoleRepository = roleRepository;
            RefreshTokenRepository = refreshTokenRepository;
            RegisterLogRepository = registerLogRepository;
            TeacherRepository = teacherRepository;
            HabilitationRepository = habilitationRepository;
            TeacherHabilitationRepository = teacherHabilitationRepository;
            ClassesRepository = classesRepository;
            ClassroomRepository = classroomRepository;
            HolidayRepository = holidayRepository;
            ScheduleRepository = scheduleRepository;
            AttendanceRepository = attendanceRepository;
        }

        public async Task CommitAsync()
        {
            try
            {
                _logger.LogInformation("Committing changes to the database in UnitOfWork at {Date}", DateTime.UtcNow);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Changes committed successfully in UnitOfWork at {Date}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("An error occurred while committing changes in UnitOfWork at {Date}", DateTime.UtcNow);
                _logger.LogError(ex, "Exception details: {Message}", ex.Message);
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                _logger.LogInformation("Disposing UnitOfWork and its resources in {Date}", DateTime.UtcNow);
                _context.Dispose();
                _logger.LogInformation("UnitOfWork disposed successfully at {Date}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("An error occurred while disposing UnitOfWork at {Date}", DateTime.UtcNow);
                _logger.LogError(ex, "Exception details: {Message}", ex.Message);
                throw;
            }
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                _logger.LogInformation("Asynchronously disposing UnitOfWork and its resources in {Date}", DateTime.UtcNow);
                await _context.DisposeAsync();
                _logger.LogInformation("UnitOfWork asynchronously disposed successfully at {Date}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("An error occurred while asynchronously disposing UnitOfWork at {Date}", DateTime.UtcNow);
                _logger.LogError(ex, "Exception details: {Message}", ex.Message);
                throw;
            }
        }
    }
}
