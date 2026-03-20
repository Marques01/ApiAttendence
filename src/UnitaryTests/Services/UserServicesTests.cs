using Application.Models;
using Application.Models.Request;
using Application.Models.Response;
using Application.Services.Users.Interfaces;
using Moq;
using System.Net;

namespace UnitaryTests.Services
{
    public class UserServicesTests
    {
        private readonly Mock<IUserServices> _userServicesMock;

        public UserServicesTests()
        {
            _userServicesMock = new Mock<IUserServices>();
        }

        [Fact]
        public async Task SignUpShouldReturnSuccessResponse()
        {
            // Arrange
            var expectedResponse = new UserResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _userServicesMock.Setup(x => x.SignUpAsync(It.IsAny<UserCostumerModel>()))
                .ReturnsAsync(expectedResponse);

            var userService = _userServicesMock.Object;
            var userRequest = new UserCostumerModel
            {
                Login = "testuser",
                Password = "123456"
            };

            // Act
            var response = await userService.SignUpAsync(userRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task SignUpShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new UserResponseModel
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            _userServicesMock.Setup(x => x.SignUpAsync(It.IsAny<UserCostumerModel>()))
                .ReturnsAsync(expectedResponse);

            var userService = _userServicesMock.Object;
            var userRequest = new UserCostumerModel
            {
                Login = "testuser",
                Password = "123456"
            };

            // Act
            var response = await userService.SignUpAsync(userRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task RefreshTokenShouldReturnSuccessResponse()
        {
            // Arrange
            var expectedResponse = new UserTokenResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK
            };

            _userServicesMock.Setup(x => x.RefreshTokenAsync(It.IsAny<RefreshTokenRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var userService = _userServicesMock.Object;
            var refreshTokenRequest = new RefreshTokenRequestModel
            {
                Token = "token",
                RefreshToken = "refreshToken"
            };

            // Act
            var response = await userService.RefreshTokenAsync(refreshTokenRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task RefreshTokenShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new UserTokenResponseModel
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            _userServicesMock.Setup(x => x.RefreshTokenAsync(It.IsAny<RefreshTokenRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var userService = _userServicesMock.Object;
            var refreshTokenRequest = new RefreshTokenRequestModel
            {
                Token = "token",
                RefreshToken = "refreshToken"
            };

            // Act
            var response = await userService.RefreshTokenAsync(refreshTokenRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task SignInShouldReturnSuccessResponse()
        {
            // Arrange
            var expectedResponse = new UserTokenResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK
            };

            _userServicesMock.Setup(x => x.SigninAsync(It.IsAny<UserRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var userService = _userServicesMock.Object;
            var userRequest = new UserRequestModel
            {
                Login = "testuser",
                Password = "123456"
            };

            // Act
            var response = await userService.SigninAsync(userRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task SignInShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new UserTokenResponseModel
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            _userServicesMock.Setup(x => x.SigninAsync(It.IsAny<UserRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var userService = _userServicesMock.Object;
            var userRequest = new UserRequestModel
            {
                Login = "testuser",
                Password = ""
            };

            // Act
            var response = await userService.SigninAsync(userRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
