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
    public class HabilitationController : ControllerBase
    {
        private readonly IHabilitationServices _habilitationServices;
        private readonly IRegistersLogsServices _registersLogsServices;

        public HabilitationController(IHabilitationServices habilitationServices, IRegistersLogsServices registersLogsServices)
        {
            _habilitationServices = habilitationServices;
            _registersLogsServices = registersLogsServices;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] HabilitationRequestModel habilitationRequestModel)
        {
            try
            {
                var habilitationResponse = await _habilitationServices.CreateAsync(habilitationRequestModel);

                await _registersLogsServices.CreateAsync(
                    message: "Cadastro de habilidade",
                    details: $"Usuário: {ExtractUserNameFromToken()} realizou o cadastro de uma nova habilidade. RequestModel: {JsonUtils.Serialize(habilitationRequestModel)}\nResponseModel: {JsonUtils.Serialize(habilitationResponse)}",
                    origin: $@"{nameof(HabilitationController)}\CreateAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return CreatedAtAction(nameof(CreateAsync), habilitationResponse);
            }
            catch (ArgumentException args)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Falha ao cadastrar habilidade (argumento inválido)",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar uma habilidade. Erro: {args.Message}",
                    origin: $@"{nameof(HabilitationController)}\CreateAsync",
                    exception: args.Message,
                    stacktrace: args.StackTrace ?? string.Empty,
                    inner: args.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new HabilitationResponseModel
                {
                    IsSuccess = false,
                    Message = args.Message,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (CustomerValidationException val)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro de validação ao cadastrar habilidade",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar uma habilidade. Erros: {JsonUtils.Serialize(val.ErrorMessages)}",
                    origin: $@"{nameof(HabilitationController)}\CreateAsync",
                    exception: string.Join("; ", val.ErrorMessages),
                    stacktrace: val.StackTrace ?? string.Empty,
                    inner: val.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new HabilitationResponseModel
                {
                    IsSuccess = false,
                    Message = string.Join("; ", val.ErrorMessages),
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro inesperado ao cadastrar habilidade",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar uma habilidade.",
                    origin: $@"{nameof(HabilitationController)}\CreateAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new HabilitationResponseModel
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