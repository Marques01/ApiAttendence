using Application.Models.Request;
using Application.Models.Response;
using Application.Services.Interfaces;
using Application.Services.RegistersLogs.Interfaces;
using Domain.CostumerExceptions;
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
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayServices _holidayServices;
        private readonly IRegistersLogsServices _registersLogsServices;

        public HolidayController(IHolidayServices holidayServices, IRegistersLogsServices registersLogsServices)
        {
            _holidayServices = holidayServices;
            _registersLogsServices = registersLogsServices;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] HolidayRequestModel holidayRequestModel)
        {
            try
            {
                var holidayResponse = await _holidayServices.CreateAsync(holidayRequestModel);

                await _registersLogsServices.CreateAsync(
                    message: "Cadastro de feriado",
                    details: $"Usuário: {ExtractUserNameFromToken()} realizou o cadastro de um novo feriado. RequestModel: {JsonUtils.Serialize(holidayRequestModel)}\nResponseModel: {JsonUtils.Serialize(holidayResponse)}",
                    origin: $@"{nameof(HolidayController)}\CreateAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return CreatedAtAction(nameof(CreateAsync), holidayResponse);
            }
            catch (ArgumentException args)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Falha ao cadastrar feriado (argumento inválido)",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar um feriado. Erro: {args.Message}",
                    origin: $@"{nameof(HolidayController)}\CreateAsync",
                    exception: args.Message,
                    stacktrace: args.StackTrace ?? string.Empty,
                    inner: args.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new HolidayResponseModel
                {
                    IsSuccess = false,
                    Message = args.Message,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (CustomerValidationException val)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro de validação ao cadastrar feriado",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar um feriado. Erros: {JsonUtils.Serialize(val.ErrorMessages)}",
                    origin: $@"{nameof(HolidayController)}\CreateAsync",
                    exception: string.Join("; ", val.ErrorMessages),
                    stacktrace: val.StackTrace ?? string.Empty,
                    inner: val.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new HolidayResponseModel
                {
                    IsSuccess = false,
                    Message = string.Join("; ", val.ErrorMessages),
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro inesperado ao cadastrar feriado",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar um feriado.",
                    origin: $@"{nameof(HolidayController)}\CreateAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new HolidayResponseModel
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