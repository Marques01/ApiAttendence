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
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentServices;
        private readonly IRegistersLogsServices _registersLogsServices;

        public StudentController(IStudentServices studentServices, IRegistersLogsServices registersLogsServices)
        {
            _studentServices = studentServices;
            _registersLogsServices = registersLogsServices;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] StudentRequestModel studentRequestModel)
        {
            try
            {
                var studentResponse = await _studentServices.CreateAsync(studentRequestModel);

                await _registersLogsServices.CreateAsync(
                    message: "Cadastro de estudante",
                    details: $"Usuário: {ExtractUserNameFromToken()} realizou o cadastro de um novo estudante. RequestModel: {JsonUtils.Serialize(studentRequestModel)}\nResponseModel: {JsonUtils.Serialize(studentResponse)}",
                    origin: $@"{nameof(StudentController)}\CreateAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return CreatedAtAction(nameof(CreateAsync), studentResponse);
            }
            catch (ArgumentException args)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Falha ao cadastrar estudante (argumento inválido)",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar um estudante. Erro: {args.Message}",
                    origin: $@"{nameof(StudentController)}\CreateAsync",
                    exception: args.Message,
                    stacktrace: args.StackTrace ?? string.Empty,
                    inner: args.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new StudentResponseModel
                {
                    IsSuccess = false,
                    Message = args.Message,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (CustomerValidationException val)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro de validação ao cadastrar estudante",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar um estudante. Erros: {JsonUtils.Serialize(val.ErrorMessages)}",
                    origin: $@"{nameof(StudentController)}\CreateAsync",
                    exception: string.Join("; ", val.ErrorMessages),
                    stacktrace: val.StackTrace ?? string.Empty,
                    inner: val.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new StudentResponseModel
                {
                    IsSuccess = false,
                    Message = string.Join("; ", val.ErrorMessages),
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro inesperado ao cadastrar estudante",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar um estudante.",
                    origin: $@"{nameof(StudentController)}\CreateAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new StudentResponseModel
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