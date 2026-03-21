using Application.Services;
using Application.Services.Interfaces;
using Application.Services.RegistersLogs;
using Application.Services.RegistersLogs.Interfaces;
using Application.Services.Tokens;
using Application.Services.Tokens.Interfaces;
using Application.Services.Users;
using Application.Services.Users.Interfaces;
using Domain.Repository.Interfaces;
using Domain.Security.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Security;

namespace API.Configurations
{
    public class DependencyInjection
    {
        public static void ApplyConfigurations(IServiceCollection services)
        {
            services.AddScoped<IStudentServices, StudentServices>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherServices, TeacherServices>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IHabilitationServices, HabilitationServices>();
            services.AddScoped<IHabilitationRepository, HabilitationRepository>();
            services.AddScoped<ITeacherHabilitationServices, TeacherHabilitationServices>();
            services.AddScoped<ITeacherHabilitationRepository, TeacherHabilitationRepository>();
            services.AddScoped<IClassesServices, ClassesServices>();
            services.AddScoped<IClassesRepository, ClassesRepository>();
            services.AddScoped<IClassroomServices, ClassroomServices>();
            services.AddScoped<IClassroomRepository, ClassroomRepository>();
            services.AddScoped<IHolidayServices, HolidayServices>();
            services.AddScoped<IHolidayRepository, HolidayRepository>();
            services.AddScoped<IScheduleServices, ScheduleServices>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IRfidCardRepository, RfidCardRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRolesRepository, UserRolesRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IRegisterLogRepository, RegisterLogRepository>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddScoped<IRegistersLogsServices, RegistersLogsServices>();
            services.AddScoped<IEncryption, Encryption>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
