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
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomServices _classroomServices;
        private readonly IRegistersLogsServices _registersLogsServices;

        public ClassroomController(IClassroomServices classroomServices, IRegistersLogsServices registersLogsServices)
        {
            _classroomServices = classroomServices;
            _registersLogsServices = registersLogsServices;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] ClassroomRequestModel classroomRequestModel)
        {
            try
            {
                var classroomResponse = await _classroomServices.CreateAsync(classroomRequestModel);

                await _registersLogsServices.CreateAsync(
                    message: "Cadastro de sala de aula",
                    details: $"Usuário: {ExtractUserNameFromToken()} realizou o cadastro de uma nova sala. RequestModel: {JsonUtils.Serialize(classroomRequestModel)}\nResponseModel: {JsonUtils.Serialize(classroomResponse)}",
                    origin: $@"{nameof(ClassroomController)}\CreateAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return CreatedAtAction(nameof(CreateAsync), classroomResponse);
            }
            catch (ArgumentException args)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Falha ao cadastrar sala (argumento inválido)",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar uma sala. Erro: {args.Message}",
                    origin: $@"{nameof(ClassroomController)}\CreateAsync",
                    exception: args.Message,
                    stacktrace: args.StackTrace ?? string.Empty,
                    inner: args.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new ClassroomResponseModel
                {
                    IsSuccess = false,
                    Message = args.Message,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (CustomerValidationException val)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro de validação ao cadastrar sala",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar uma sala. Erros: {JsonUtils.Serialize(val.ErrorMessages)}",
                    origin: $@"{nameof(ClassroomController)}\CreateAsync",
                    exception: string.Join("; ", val.ErrorMessages),
                    stacktrace: val.StackTrace ?? string.Empty,
                    inner: val.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new ClassroomResponseModel
                {
                    IsSuccess = false,
                    Message = string.Join("; ", val.ErrorMessages),
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro inesperado ao cadastrar sala",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar uma sala.",
                    origin: $@"{nameof(ClassroomController)}\CreateAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new ClassroomResponseModel
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