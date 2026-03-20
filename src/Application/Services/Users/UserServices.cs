using Application.Models;
using Application.Models.Factories;
using Application.Models.Request;
using Application.Models.Response;
using Application.Services.Tokens.Interfaces;
using Application.Services.Users.Interfaces;
using Application.Validators;
using Domain.CostumerExceptions;
using Domain.Entities;
using Domain.Repository.Interfaces;
using Domain.Security.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;

namespace Application.Services.Users
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _uof;

        private readonly ITokenServices _tokenServices;

        private readonly IEncryption _encryption;

        public UserServices(IUnitOfWork uof, ITokenServices tokenServices, IEncryption encryption)
        {
            _uof = uof;
            _tokenServices = tokenServices;
            _encryption = encryption;
        }

        public async Task<UserResponseModel> SignUpAsync(UserCostumerModel userCostumerModel)
        {
            try
            {
                IsValid(userCostumerModel);

                bool userExists = await _uof.UserRepository.UserExistingAsync(userCostumerModel.Login.Trim().ToLower());

                if (userExists)
                    throw new ArgumentException("Este login já está associado a uma conta existente.");

                if (userCostumerModel.Password != userCostumerModel.ConfirmPassword)
                    throw new ArgumentException("As senhas não coincidem.");

                string salt = _encryption.GenerateSalt();
                string password = _encryption.GenerateHash(userCostumerModel.Password, salt);

                var user = UserCostumerFactory.CreateFromUserCostumerModel(userCostumerModel);
                user.Password = password;
                user.Salt = salt;

                await _uof.UserRepository.CreateAsync(user);
                await _uof.CommitAsync();

                return new UserResponseModel()
                {
                    IsSuccess = true,
                    Message = "Usuário cadastrado com sucesso.",
                    StatusCode = HttpStatusCode.Created,
                    Model = user
                };
            }
            catch (CustomerValidationException validationException)
            {
                throw new CustomerValidationException(validationException.ErrorMessages);
            }
            catch (ArgumentException arg)
            {
                throw new ArgumentException(arg.Message);
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro ao realizar o login");
            }
        }

        public async Task<UserTokenResponseModel> RefreshTokenAsync(RefreshTokenRequestModel refreshTokenRequestModel)
        {
            try
            {
                var userToken = await _tokenServices.RefreshTokenAsync(refreshTokenRequestModel.Token,
                    refreshTokenRequestModel.RefreshToken);

                return new()
                {
                    IsSuccess = true,
                    Message = "Login realizado com sucesso.",
                    Model = userToken,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (SecurityTokenException securityToken)
            {
                throw new SecurityTokenException(securityToken.Message);
            }
            catch (ArgumentException arg)
            {
                throw new ArgumentException(arg.Message);
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro ao realizar o login");
            }
        }

        public async Task<UserTokenResponseModel> SigninAsync(UserRequestModel userRequestModel)
        {
            try
            {
                var userResult = await FindUserAsync(userRequestModel.Login);

                if (userResult.UserId == 0)
                    throw new ArgumentException("Usuário não encontrado");

                bool passwordIsValid = _encryption.VerifyHash(
                    userRequestModel.Password, userResult.Salt, userResult.Password);

                if (!passwordIsValid && userResult.UserId > 0 && userResult.FailedCount < 3)
                {
                    await IncrementFailedLoginAttempts(userResult.Login);
                    throw new ArgumentException("Credenciais incorretas. Por favor, tente novamente.");
                }

                if (userResult is { UserId: > 0, Enabled: false })
                {
                    await IncrementFailedLoginAttempts(userResult.Login);

                    throw new ArgumentException(
                        "Conta desativada. Entre em contato com o administrador para mais informações.");
                }

                if (userResult is { UserId: > 0, FailedCount: >= 3 })
                {
                    throw new ArgumentException("Usuário bloqueado por tentativas inválidas.");
                }

                await UpdateLastLoginAsync(userResult.UserId);

                var claims = await _tokenServices.GetUserRolesAsync(userResult);

                var token = GenerateToken(claims, userRequestModel.RememberMe);

                if (!string.IsNullOrEmpty(token.RefreshToken))
                {
                    RefreshToken refreshToken = new()
                    {
                        Token = token.RefreshToken,
                        Expiration = token.Expiration,
                        CreatAt = token.CreatAt
                    };

                    await _uof.RefreshTokenRepository.CreateAsync(refreshToken);

                    await _uof.CommitAsync();
                }

                return new()
                {
                    IsSuccess = true,
                    Message = "Login realizado com sucesso!",
                    Model = token,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (ArgumentException arg)
            {
                throw new ArgumentException(arg.Message);
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro ao realizar o login");
            }
        }

        private void IsValid(UserCostumerModel userCostumerModel)
        {
            UserCostumerModelValidator costumerModelValidator = new UserCostumerModelValidator(userCostumerModel);

            var errorMessages = costumerModelValidator.GetErrorMessages().ToList();

            bool isValid = errorMessages.Count() == 0;

            if (isValid)
                return;

            throw new CustomerValidationException(errorMessages);
        }

        private async Task IncrementFailedLoginAttempts(string login)
        {
            var user = await GetUserByLoginAsync(login);
            user.FailedCount++;

            await _uof.CommitAsync();
        }

        private async Task UpdateLastLoginAsync(int userId)
        {
            var user = await _uof.UserRepository.GetUserByIdAsync(userId);
            user.LastLogin = DateTime.Now;

            await _uof.CommitAsync();
        }

        private async Task<User> GetUserByLoginAsync(string login)
        {
            try
            {
                var user = await _uof.UserRepository.GetUserByLoginAsync(login);

                if (user.UserId == 0)
                    throw new ArgumentException("Usuário não encontrado");

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<User> FindUserAsync(string login)
        {
            var userResult = await _uof.UserRepository.GetUserByLoginAsync(login);

            return userResult;
        }

        private UserToken GenerateToken(IEnumerable<Claim> claims, bool remember)
        {
            return _tokenServices.GenerateToken(claims, remember);
        }
    }
}