using Application.Models;
using Application.Models.Request;
using Application.Models.Response;
using Application.Services.RegistersLogs.Interfaces;
using Application.Services.Users.Interfaces;
using Domain.CostumerExceptions;
using Domain.Enum;
using Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IRegistersLogsServices _registersLogsServices;

        public AccountController(IUserServices userServices, IRegistersLogsServices registersLogsServices)
        {
            _userServices = userServices;
            _registersLogsServices = registersLogsServices;
        }

        [HttpPost("signin")]
        public async Task<ActionResult> Signin([FromBody] UserRequestModel userRequestModel)
        {
            try
            {
                var userSignIn = await _userServices.SigninAsync(userRequestModel);

                var userTokenModel = userSignIn.Model is null ? null : (UserToken)userSignIn.Model;

                if (userTokenModel is not null && !string.IsNullOrEmpty(userTokenModel.RefreshToken))
                {                    
                    SetRefreshTokenCookie(Response, userTokenModel.RefreshToken);
                }

                await _registersLogsServices.CreateAsync(
                    message: "Login de usuário",
                    details: $"Usuário: {userRequestModel.Login} realizou login com sucesso. ResponseModel: {JsonUtils.Serialize(userSignIn)}",
                    origin: $@"{nameof(AccountController)}\Signin",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return Ok(userSignIn);
            }
            catch (ArgumentException arg)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Falha no login de usuário (argumento inválido)",
                    details: $"Usuário: {userRequestModel.Login} falhou ao realizar login. Erro: {arg.Message}",
                    origin: $@"{nameof(AccountController)}\Signin",
                    exception: arg.Message,
                    stacktrace: arg.StackTrace ?? string.Empty,
                    inner: arg.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new UserResponseModel
                {
                    IsSuccess = false,
                    Message = arg.Message,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (CustomerValidationException val)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro de validação no login de usuário",
                    details: $"Usuário: {userRequestModel.Login} falhou ao realizar login. Erros: {JsonUtils.Serialize(val.ErrorMessages)}",
                    origin: $@"{nameof(AccountController)}\Signin",
                    exception: string.Join("; ", val.ErrorMessages),
                    stacktrace: val.StackTrace ?? string.Empty,
                    inner: val.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new UserResponseModel
                {
                    IsSuccess = false,
                    Message = string.Join("; ", val.ErrorMessages),
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro inesperado no login de usuário",
                    details: $"Usuário: {userRequestModel.Login} encontrou um erro inesperado ao realizar login.",
                    origin: $@"{nameof(AccountController)}\Signin",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new UserResponseModel
                {
                    IsSuccess = false,
                    Message = "Ocorreu um erro em nossos servidores. Tente novamente mais tarde",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpPost("register")]
        //[Authorize]
        public async Task<ActionResult> CreateAsync([FromBody] UserCostumerModel userCostumerModel)
        {
            try
            {
                var userResponse = await _userServices.SignUpAsync(userCostumerModel);

                await _registersLogsServices.CreateAsync(
                    message: "Cadastro de usuário",
                    details: $"Usuário: {ExtractUserNameFromToken()} realizou o cadastro de um novo usuário. RequestModel: {JsonUtils.Serialize(userCostumerModel)}\nResponseModel: {JsonUtils.Serialize(userResponse)}",
                    origin: $@"{nameof(AccountController)}\CreateAsync",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return Ok(userResponse);
            }
            catch (ArgumentException args)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Falha ao cadastrar usuário (argumento inválido)",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar um usuário. Erro: {args.Message}",
                    origin: $@"{nameof(AccountController)}\CreateAsync",
                    exception: args.Message,
                    stacktrace: args.StackTrace ?? string.Empty,
                    inner: args.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new UserResponseModel
                {
                    IsSuccess = false,
                    Message = args.Message,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (CustomerValidationException val)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro de validação ao cadastrar usuário",
                    details: $"Usuário: ExtractUserNameFromToken() tentou cadastrar um usuário. Erros: {JsonUtils.Serialize(val.ErrorMessages)}",
                    origin: $@"{nameof(AccountController)}\CreateAsync",
                    exception: string.Join("; ", val.ErrorMessages),
                    stacktrace: val.StackTrace ?? string.Empty,
                    inner: val.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new UserResponseModel
                {
                    IsSuccess = false,
                    Message = string.Join("; ", val.ErrorMessages),
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro inesperado ao cadastrar usuário",
                    details: $"Usuário: {ExtractUserNameFromToken()} tentou cadastrar um usuário.",
                    origin: $@"{nameof(AccountController)}\CreateAsync",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new UserResponseModel
                {
                    IsSuccess = false,
                    Message = "Ocorreu um erro em nossos servidores. Tente novamente mais tarde",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpPost("refresh")]
        [Authorize]
        public async Task<ActionResult> RefreshToken([FromBody] UserTokenRequestModel userTokenModel)
        {
            try
            {
                var authorizationHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authorizationHeader) ||
                    !authorizationHeader.StartsWith("Bearer ", StringComparison.CurrentCultureIgnoreCase))
                {
                    var responseModel = new UserTokenResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.Unauthorized,
                        Message = "Token JWT ausente ou inválido."
                    };

                    await _registersLogsServices.CreateAsync(
                        message: "Falha ao atualizar token JWT (sem autorização)",
                        details: $"Cabeçalho de autorização inválido. RequestModel: {JsonUtils.Serialize(userTokenModel)}",
                        origin: $@"{nameof(AccountController)}\RefreshToken",
                        exception: "Token JWT ausente ou inválido.",
                        stacktrace: string.Empty,
                        inner: string.Empty,
                        situationEnum: SituationEnum.Warning);

                    return StatusCode(StatusCodes.Status401Unauthorized, responseModel);
                }

                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                var refreshToken = userTokenModel.RefreshToken;

                if (string.IsNullOrEmpty(refreshToken))
                {
                    var responseModel = new UserTokenResponseModel
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.Unauthorized,
                        Message = "Refresh token não encontrado. Por favor, autentique-se novamente."
                    };

                    await _registersLogsServices.CreateAsync(
                        message: "Falha ao atualizar token JWT (refresh token ausente)",
                        details: $"Token principal: {token}. Refresh token ausente.",
                        origin: $@"{nameof(AccountController)}\RefreshToken",
                        exception: "Refresh token ausente.",
                        stacktrace: string.Empty,
                        inner: string.Empty,
                        situationEnum: SituationEnum.Warning);

                    return StatusCode(StatusCodes.Status401Unauthorized, responseModel);
                }

                var refreshTokenRequestModel = new RefreshTokenRequestModel
                {
                    Token = token,
                    RefreshToken = refreshToken
                };

                var userToken = await _userServices.RefreshTokenAsync(refreshTokenRequestModel);

                await _registersLogsServices.CreateAsync(
                    message: "Atualização de token JWT",
                    details: $"Usuário atualizou o token JWT com sucesso. RequestModel: {JsonUtils.Serialize(refreshTokenRequestModel)}\nResponseModel: {JsonUtils.Serialize(userToken)}",
                    origin: $@"{nameof(AccountController)}\RefreshToken",
                    exception: string.Empty,
                    stacktrace: string.Empty,
                    inner: string.Empty,
                    situationEnum: SituationEnum.Completed);

                return Ok(userToken);
            }
            catch (SecurityTokenException ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Token JWT expirado",
                    details: $"Erro: {ex.Message}",
                    origin: $@"{nameof(AccountController)}\RefreshToken",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Warning);

                return BadRequest(new UserTokenResponseModel
                {
                    IsSuccess = false,
                    Message = "Token expirado.",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (Exception ex)
            {
                await _registersLogsServices.CreateAsync(
                    message: "Erro inesperado ao atualizar token JWT",
                    details: $"Erro: {ex.Message}",
                    origin: $@"{nameof(AccountController)}\RefreshToken",
                    exception: ex.Message,
                    stacktrace: ex.StackTrace ?? string.Empty,
                    inner: ex.InnerException?.ToString() ?? string.Empty,
                    situationEnum: SituationEnum.Error);

                return StatusCode(StatusCodes.Status500InternalServerError, new UserTokenResponseModel
                {
                    IsSuccess = false,
                    Message = "Ocorreu um erro em nossos servidores. Tente novamente mais tarde",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        private void SetRefreshTokenCookie(HttpResponse response, string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(1)
            };

            response.Cookies.Append("RefreshToken", token, cookieOptions);
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
