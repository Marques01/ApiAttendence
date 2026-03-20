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
    public class RfidCardController : ControllerBase
    {
        private readonly IRfidCardServices _rfidCardServices;
        private readonly IRegistersLogsServices _registersLogsServices;

        public RfidCardController(IRfidCardServices rfidCardServices, IRegistersLogsServices registersLogsServices)
        {
            _rfidCardServices = rfidCardServices;
            _registersLogsServices = registersLogsServices;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] RfidCardRequestModel rfidCardRequestModel)
        {
            try
            {
                var rfidCardResponse = await _rfidCardServices.CreateAsync(rfidCardRequestModel);

                await _registersLogsServices.CreateAsync(
                    message: "Cadastro de cartão RFID",
                    details: $"Usuário: {ExtractUserNameFromToken()} realizou o cadastro de um novo cartão RFID. RequestModel: {JsonUtils.Serialize(rfidCardRequestModel)}\nResponseModel: {JsonUtils.Serialize(rfidCardResponse)}",
                    origin: $@"{nameof(RfidCardController)}\CreateAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return CreatedAtAction(nameof(CreateAsync), rfidCardResponse);
            }
            catch (ArgumentException args)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Falha ao cadastrar cartão RFID (argumento inválido)",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar um cartão RFID. Erro: {args.Message}",
                    origin: $@"{nameof(RfidCardController)}\CreateAsync",
                    exception: args.Message,
                    stacktrace: args.StackTrace ?? string.Empty,
                    inner: args.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new RfidCardResponseModel
                {
                    IsSuccess = false,
                    Message = args.Message,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (CustomerValidationException val)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro de validação ao cadastrar cartão RFID",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar um cartão RFID. Erros: {JsonUtils.Serialize(val.ErrorMessages)}",
                    origin: $@"{nameof(RfidCardController)}\CreateAsync",
                    exception: string.Join("; ", val.ErrorMessages),
                    stacktrace: val.StackTrace ?? string.Empty,
                    inner: val.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new RfidCardResponseModel
                {
                    IsSuccess = false,
                    Message = string.Join("; ", val.ErrorMessages),
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro inesperado ao cadastrar cartão RFID",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar um cartão RFID.",
                    origin: $@"{nameof(RfidCardController)}\CreateAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new RfidCardResponseModel
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