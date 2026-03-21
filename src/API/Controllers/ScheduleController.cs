using Application.Models.Response;
using Application.Services.Interfaces;
using Application.Services.RegistersLogs.Interfaces;
using Domain.Enum;
using Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleServices _scheduleServices;
        private readonly IRegistersLogsServices _registersLogsServices;

        public ScheduleController(IScheduleServices scheduleServices, IRegistersLogsServices registersLogsServices)
        {
            _scheduleServices = scheduleServices;
            _registersLogsServices = registersLogsServices;
        }

        [HttpPost("generate/{classId}")]
        public async Task<ActionResult> GenerateSchedulesAsync(int classId, [FromQuery] TimeOnly startTime, [FromQuery] TimeOnly endTime)
        {
            try
            {
                var response = await _scheduleServices.GenerateSchedulesAsync(classId, startTime, endTime);

                await _registersLogsServices.CreateAsync(
                    message: "Geração de agendamentos de aula",
                    details: $"Usuário: {ExtractUserNameFromToken()} gerou agendamentos para a classe {classId}. StartTime: {startTime}, EndTime: {endTime}. Response: {JsonUtils.Serialize(response)}",
                    origin: $@"{nameof(ScheduleController)}\GenerateSchedulesAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return Ok(response);
            }
            catch (ArgumentException args)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Falha ao gerar agendamentos (argumento inválido)",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou gerar agendamentos. Erro: {args.Message}",
                    origin: $@"{nameof(ScheduleController)}\GenerateSchedulesAsync",
                    exception: args.Message,
                    stacktrace: args.StackTrace ?? string.Empty,
                    inner: args.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new ScheduleResponseModel
                {
                    IsSuccess = false,
                    Message = args.Message,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro inesperado ao gerar agendamentos",
                    details: $"Usuário: {ExtractUserNameFromToken()} encontrou um erro ao gerar agendamentos.",
                    origin: $@"{nameof(ScheduleController)}\GenerateSchedulesAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new ScheduleResponseModel
                {
                    IsSuccess = false,
                    Message = "Ocorreu um erro em nossos servidores. Tente novamente mais tarde",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpGet("class/{classId}")]
        public async Task<ActionResult> GetSchedulesByClassIdAsync(int classId)
        {
            try
            {
                var response = await _scheduleServices.GetSchedulesByClassIdAsync(classId);

                await _registersLogsServices.CreateAsync(
                    message: "Consulta de agendamentos por classe",
                    details: $"Usuário: {ExtractUserNameFromToken()} consultou agendamentos da classe {classId}",
                    origin: $@"{nameof(ScheduleController)}\GetSchedulesByClassIdAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return Ok(response);
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro ao consultar agendamentos por classe",
                    details: $"Usuário: {ExtractUserNameFromToken()} encontrou um erro.",
                    origin: $@"{nameof(ScheduleController)}\GetSchedulesByClassIdAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new ScheduleResponseModel
                {
                    IsSuccess = false,
                    Message = "Ocorreu um erro em nossos servidores. Tente novamente mais tarde",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpGet("teacher/{teacherId}")]
        public async Task<ActionResult> GetSchedulesByTeacherIdAsync(int teacherId)
        {
            try
            {
                var response = await _scheduleServices.GetSchedulesByTeacherIdAsync(teacherId);

                await _registersLogsServices.CreateAsync(
                    message: "Consulta de agendamentos por professor",
                    details: $"Usuário: {ExtractUserNameFromToken()} consultou agendamentos do professor {teacherId}",
                    origin: $@"{nameof(ScheduleController)}\GetSchedulesByTeacherIdAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return Ok(response);
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro ao consultar agendamentos por professor",
                    details: $"Usuário: {ExtractUserNameFromToken()} encontrou um erro.",
                    origin: $@"{nameof(ScheduleController)}\GetSchedulesByTeacherIdAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new ScheduleResponseModel
                {
                    IsSuccess = false,
                    Message = "Ocorreu um erro em nossos servidores. Tente novamente mais tarde",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpGet("range")]
        public async Task<ActionResult> GetSchedulesByDateRangeAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var response = await _scheduleServices.GetSchedulesByDateRangeAsync(startDate, endDate);

                await _registersLogsServices.CreateAsync(
                    message: "Consulta de agendamentos por período",
                    details: $"Usuário: {ExtractUserNameFromToken()} consultou agendamentos entre {startDate:dd/MM/yyyy} e {endDate:dd/MM/yyyy}",
                    origin: $@"{nameof(ScheduleController)}\GetSchedulesByDateRangeAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return Ok(response);
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro ao consultar agendamentos por período",
                    details: $"Usuário: {ExtractUserNameFromToken()} encontrou um erro.",
                    origin: $@"{nameof(ScheduleController)}\GetSchedulesByDateRangeAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new ScheduleResponseModel
                {
                    IsSuccess = false,
                    Message = "Ocorreu um erro em nossos servidores. Tente novamente mais tarde",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpGet("dayofweek/{dayOfWeek}")]
        public async Task<ActionResult> GetSchedulesByDayOfWeekAsync(int dayOfWeek)
        {
            try
            {
                if (dayOfWeek < 0 || dayOfWeek > 6)
                    return BadRequest(new ScheduleResponseModel
                    {
                        IsSuccess = false,
                        Message = "DayOfWeek inválido (0-6)",
                        StatusCode = HttpStatusCode.BadRequest
                    });

                var response = await _scheduleServices.GetSchedulesByDayOfWeekAsync((DayOfWeek)dayOfWeek);

                await _registersLogsServices.CreateAsync(
                    message: "Consulta de agendamentos por dia da semana",
                    details: $"Usuário: {ExtractUserNameFromToken()} consultou agendamentos para {(DayOfWeek)dayOfWeek}",
                    origin: $@"{nameof(ScheduleController)}\GetSchedulesByDayOfWeekAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return Ok(response);
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro ao consultar agendamentos por dia da semana",
                    details: $"Usuário: {ExtractUserNameFromToken()} encontrou um erro.",
                    origin: $@"{nameof(ScheduleController)}\GetSchedulesByDayOfWeekAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new ScheduleResponseModel
                {
                    IsSuccess = false,
                    Message = "Ocorreu um erro em nossos servidores. Tente novamente mais tarde",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpGet("active")]
        public async Task<ActionResult> GetActiveSchedulesAsync()
        {
            try
            {
                var response = await _scheduleServices.GetActiveSchedulesAsync();

                await _registersLogsServices.CreateAsync(
                    message: "Consulta de agendamentos ativos",
                    details: $"Usuário: {ExtractUserNameFromToken()} consultou todos os agendamentos ativos",
                    origin: $@"{nameof(ScheduleController)}\GetActiveSchedulesAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return Ok(response);
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro ao consultar agendamentos ativos",
                    details: $"Usuário: {ExtractUserNameFromToken()} encontrou um erro.",
                    origin: $@"{nameof(ScheduleController)}\GetActiveSchedulesAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new ScheduleResponseModel
                {
                    IsSuccess = false,
                    Message = "Ocorreu um erro em nossos servidores. Tente novamente mais tarde",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpGet("holidays")]
        public async Task<ActionResult> GetHolidaySchedulesAsync()
        {
            try
            {
                var response = await _scheduleServices.GetHolidaySchedulesAsync();

                await _registersLogsServices.CreateAsync(
                    message: "Consulta de agendamentos em feriados",
                    details: $"Usuário: {ExtractUserNameFromToken()} consultou agendamentos que caem em feriados",
                    origin: $@"{nameof(ScheduleController)}\GetHolidaySchedulesAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return Ok(response);
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro ao consultar agendamentos em feriados",
                    details: $"Usuário: {ExtractUserNameFromToken()} encontrou um erro.",
                    origin: $@"{nameof(ScheduleController)}\GetHolidaySchedulesAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new ScheduleResponseModel
                {
                    IsSuccess = false,
                    Message = "Ocorreu um erro em nossos servidores. Tente novamente mais tarde",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpGet("stats/class/{classId}")]
        public async Task<ActionResult> GetClassStatsAsync(int classId)
        {
            try
            {
                var totalDays = await _scheduleServices.GetTotalClassDaysAsync(classId);
                var holidayDays = await _scheduleServices.GetTotalHolidayDaysAsync(classId);

                var stats = new
                {
                    ClassId = classId,
                    TotalClassDays = totalDays,
                    TotalHolidayDays = holidayDays,
                    TotalDays = totalDays + holidayDays,
                    PercentageOfClassDays = totalDays + holidayDays > 0 ? (totalDays * 100.0) / (totalDays + holidayDays) : 0
                };

                await _registersLogsServices.CreateAsync(
                    message: "Consulta de estatísticas de agendamentos",
                    details: $"Usuário: {ExtractUserNameFromToken()} consultou estatísticas da classe {classId}",
                    origin: $@"{nameof(ScheduleController)}\GetClassStatsAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return Ok(stats);
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro ao consultar estatísticas",
                    details: $"Usuário: {ExtractUserNameFromToken()} encontrou um erro.",
                    origin: $@"{nameof(ScheduleController)}\GetClassStatsAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new ScheduleResponseModel
                {
                    IsSuccess = false,
                    Message = "Ocorreu um erro em nossos servidores. Tente novamente mais tarde",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        private string ExtractUserNameFromToken()
        {
            var claims = User.Claims.ToList();
            string userName = claims.FirstOrDefault(x => x.Type.Contains("Actor", StringComparison.CurrentCultureIgnoreCase))?.Value ?? string.Empty;

            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("Nome de usuário não encontrado no token.");

            return userName;
        }
    }
}