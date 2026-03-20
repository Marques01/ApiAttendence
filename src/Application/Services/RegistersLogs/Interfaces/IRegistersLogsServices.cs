using Domain.Enum;

namespace Application.Services.RegistersLogs.Interfaces
{
    public interface IRegistersLogsServices
    {
        Task CreateAsync(string message, string details, string origin, string exception, string stacktrace, string inner, SituationEnum situationEnum);
    }
}
