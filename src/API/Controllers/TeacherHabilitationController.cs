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
    public class TeacherHabilitationController : ControllerBase
    {
        private readonly ITeacherHabilitationServices _teacherHabilitationServices;
        private readonly IRegistersLogsServices _registersLogsServices;

        public TeacherHabilitationController(ITeacherHabilitationServices teacherHabilitationServices, IRegistersLogsServices registersLogsServices)
        {
            _teacherHabilitationServices = teacherHabilitationServices;
            _registersLogsServices = registersLogsServices;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] TeacherHabilitationRequestModel teacherHabilitationRequestModel)
        {
            try
            {
                var teacherHabilitationResponse = await _teacherHabilitationServices.CreateAsync(teacherHabilitationRequestModel);

                await _registersLogsServices.CreateAsync(
                    message: "Vinculação de habilidade ao professor",
                    details: $"Usuário: {ExtractUserNameFromToken()} vinculou uma habilidade a um professor. RequestModel: {JsonUtils.Serialize(teacherHabilitationRequestModel)}\nResponseModel: {JsonUtils.Serialize(teacherHabilitationResponse)}",
                    origin: $@"{nameof(TeacherHabilitationController)}\CreateAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return CreatedAtAction(nameof(CreateAsync), teacherHabilitationResponse);
            }
            catch (ArgumentException args)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Falha ao vincular habilidade ao professor (argumento inválido)",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou vincular uma habilidade. Erro: {args.Message}",
                    origin: $@"{nameof(TeacherHabilitationController)}\CreateAsync",
                    exception: args.Message,
                    stacktrace: args.StackTrace ?? string.Empty,
                    inner: args.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new TeacherHabilitationResponseModel
                {
                    IsSuccess = false,
                    Message = args.Message,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (CustomerValidationException val)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro de validação ao vincular habilidade",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou vincular uma habilidade. Erros: {JsonUtils.Serialize(val.ErrorMessages)}",
                    origin: $@"{nameof(TeacherHabilitationController)}\CreateAsync",
                    exception: string.Join("; ", val.ErrorMessages),
                    stacktrace: val.StackTrace ?? string.Empty,
                    inner: val.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new TeacherHabilitationResponseModel
                {
                    IsSuccess = false,
                    Message = string.Join("; ", val.ErrorMessages),
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro inesperado ao vincular habilidade",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou vincular uma habilidade.",
                    origin: $@"{nameof(TeacherHabilitationController)}\CreateAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new TeacherHabilitationResponseModel
                {
                    IsSuccess = false,
                    Message = "Ocorreu um erro em nossos servidores. Tente novamente mais tarde",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var teacherHabilitationResponse = await _teacherHabilitationServices.DeleteAsync(id);

                await _registersLogsServices.CreateAsync(
                    message: "Remoção de habilidade do professor",
                    details: $"Usuário: {ExtractUserNameFromToken()} removeu uma habilidade de um professor. Id: {id}\nResponseModel: {JsonUtils.Serialize(teacherHabilitationResponse)}",
                    origin: $@"{nameof(TeacherHabilitationController)}\DeleteAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return Ok(teacherHabilitationResponse);
            }
            catch (ArgumentException args)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Falha ao remover habilidade do professor (argumento inválido)",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou remover uma habilidade. Erro: {args.Message}",
                    origin: $@"{nameof(TeacherHabilitationController)}\DeleteAsync",
                    exception: args.Message,
                    stacktrace: args.StackTrace ?? string.Empty,
                    inner: args.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new TeacherHabilitationResponseModel
                {
                    IsSuccess = false,
                    Message = args.Message,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro inesperado ao remover habilidade",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou remover uma habilidade.",
                    origin: $@"{nameof(TeacherHabilitationController)}\DeleteAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new TeacherHabilitationResponseModel
                {
                    IsSuccess = false,
                    Message = "Ocorreu um erro em nossos servidores. Tente novamente mais tarde",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpGet("teacher/{teacherId}")]
        public async Task<ActionResult> GetTeacherHabilitationsAsync(int teacherId)
        {
            try
            {
                var teacherHabilitations = await _teacherHabilitationServices.GetTeacherHabilitationsAsync(teacherId);

                await _registersLogsServices.CreateAsync(
                    message: "Consulta de habilidades do professor",
                    details: $"Usuário: {ExtractUserNameFromToken()} consultou as habilidades do professor. TeacherId: {teacherId}",
                    origin: $@"{nameof(TeacherHabilitationController)}\GetTeacherHabilitationsAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return Ok(teacherHabilitations);
            }
            catch (ArgumentException args)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Falha ao consultar habilidades do professor",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou consultar habilidades. Erro: {args.Message}",
                    origin: $@"{nameof(TeacherHabilitationController)}\GetTeacherHabilitationsAsync",
                    exception: args.Message,
                    stacktrace: args.StackTrace ?? string.Empty,
                    inner: args.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new List<TeacherHabilitationResponseModel>
                {
                    new()
                    {
                        IsSuccess = false,
                        Message = args.Message,
                        StatusCode = HttpStatusCode.BadRequest
                    }
                });
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro inesperado ao consultar habilidades",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou consultar habilidades.",
                    origin: $@"{nameof(TeacherHabilitationController)}\GetTeacherHabilitationsAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new List<TeacherHabilitationResponseModel>
                {
                    new()
                    {
                        IsSuccess = false,
                        Message = "Ocorreu um erro em nossos servidores. Tente novamente mais tarde",
                        StatusCode = HttpStatusCode.InternalServerError
                    }
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