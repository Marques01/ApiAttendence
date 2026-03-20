using Application.Models;
using Application.Models.Request;
using Application.Models.Response;

namespace Application.Services.Users.Interfaces
{
    public interface IUserServices
    {
        Task<UserTokenResponseModel> SigninAsync(UserRequestModel userRequestModel);

        Task<UserResponseModel> SignUpAsync(UserCostumerModel userCostumerModel);

        Task<UserTokenResponseModel> RefreshTokenAsync(RefreshTokenRequestModel refreshTokenRequestModel);
    }
}
