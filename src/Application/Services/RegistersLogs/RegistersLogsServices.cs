using Application.Services.RegistersLogs.Interfaces;
using Domain.Entities;
using Domain.Enum;
using Domain.Repository.Interfaces;

namespace Application.Services.RegistersLogs
{
    public class RegistersLogsServices : IRegistersLogsServices
    {
        private readonly IUnitOfWork _uof;

        public RegistersLogsServices(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task CreateAsync(string message, string details, string origin, string exception, string stacktrace, string inner, SituationEnum situationEnum)
        {
            var registerLog = MapFromStringValues(message, details, origin, exception, stacktrace, inner, situationEnum);

            await _uof.RegisterLogRepository.CreateAsync(registerLog);
            await _uof.CommitAsync();
        }

        private RegisterLog MapFromStringValues(string message, string details, string origin, string exception, string stacktrace, string inner, SituationEnum situationEnum)
        {
            return new RegisterLog
            {
                Message = message.Trim(),
                Details = details.Trim(),
                Origin = origin.Trim(),
                Exception = exception.Trim(),
                StackTrace = stacktrace.Trim(),
                Inner = inner.Trim(),
                Situation = situationEnum,
                CreateAt = DateTime.Now
            };
        }
    }
}
