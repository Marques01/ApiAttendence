using Application.Models.Request;
using Application.Models.Response;
using Application.Services.Interfaces;
using Moq;
using System.Net;

namespace UnitaryTests.Services
{
    public class TeacherHabilitationServicesTests
    {
        private readonly Mock<ITeacherHabilitationServices> _teacherHabilitationServicesMock;

        public TeacherHabilitationServicesTests()
        {
            _teacherHabilitationServicesMock = new Mock<ITeacherHabilitationServices>();
        }

        [Fact]
        public async Task CreateTeacherHabilitationShouldReturnSuccessResponse()
        {
            // Arrange
            var expectedResponse = new TeacherHabilitationResponseModel
            {
                IsSuccess = true,
                Message = "Habilidade vinculada ao professor com sucesso",
                StatusCode = HttpStatusCode.Created
            };

            _teacherHabilitationServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherHabilitationRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherHabilitationService = _teacherHabilitationServicesMock.Object;
            var teacherHabilitationRequest = new TeacherHabilitationRequestModel
            {
                TeacherId = 1,
                HabilitationId = 1,
                Enabled = true
            };

            // Act
            var response = await teacherHabilitationService.CreateAsync(teacherHabilitationRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("Habilidade vinculada ao professor com sucesso", response.Message);
        }

        [Fact]
        public async Task CreateTeacherHabilitationWithInvalidTeacherIdShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new TeacherHabilitationResponseModel
            {
                IsSuccess = false,
                Message = "Professor não encontrado",
                StatusCode = HttpStatusCode.BadRequest
            };

            _teacherHabilitationServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherHabilitationRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherHabilitationService = _teacherHabilitationServicesMock.Object;
            var teacherHabilitationRequest = new TeacherHabilitationRequestModel
            {
                TeacherId = 0,
                HabilitationId = 1,
                Enabled = true
            };

            // Act
            var response = await teacherHabilitationService.CreateAsync(teacherHabilitationRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTeacherHabilitationWithInvalidHabilitationIdShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new TeacherHabilitationResponseModel
            {
                IsSuccess = false,
                Message = "Habilidade não encontrada",
                StatusCode = HttpStatusCode.BadRequest
            };

            _teacherHabilitationServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherHabilitationRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherHabilitationService = _teacherHabilitationServicesMock.Object;
            var teacherHabilitationRequest = new TeacherHabilitationRequestModel
            {
                TeacherId = 1,
                HabilitationId = 0,
                Enabled = true
            };

            // Act
            var response = await teacherHabilitationService.CreateAsync(teacherHabilitationRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTeacherHabilitationWithDuplicateShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new TeacherHabilitationResponseModel
            {
                IsSuccess = false,
                Message = "Este professor já possui essa habilidade",
                StatusCode = HttpStatusCode.BadRequest
            };

            _teacherHabilitationServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherHabilitationRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherHabilitationService = _teacherHabilitationServicesMock.Object;
            var teacherHabilitationRequest = new TeacherHabilitationRequestModel
            {
                TeacherId = 1,
                HabilitationId = 1,
                Enabled = true
            };

            // Act
            var response = await teacherHabilitationService.CreateAsync(teacherHabilitationRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Este professor já possui essa habilidade", response.Message);
        }

        [Fact]
        public async Task CreateTeacherHabilitationWithValidDataShouldReturnCreatedStatusCode()
        {
            // Arrange
            var expectedResponse = new TeacherHabilitationResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _teacherHabilitationServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherHabilitationRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherHabilitationService = _teacherHabilitationServicesMock.Object;
            var teacherHabilitationRequest = new TeacherHabilitationRequestModel
            {
                TeacherId = 2,
                HabilitationId = 2,
                Enabled = true
            };

            // Act
            var response = await teacherHabilitationService.CreateAsync(teacherHabilitationRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateTeacherHabilitationWithDisabledStatusShouldReturnSuccess()
        {
            // Arrange
            var expectedResponse = new TeacherHabilitationResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _teacherHabilitationServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherHabilitationRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherHabilitationService = _teacherHabilitationServicesMock.Object;
            var teacherHabilitationRequest = new TeacherHabilitationRequestModel
            {
                TeacherId = 3,
                HabilitationId = 3,
                Enabled = false
            };

            // Act
            var response = await teacherHabilitationService.CreateAsync(teacherHabilitationRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task DeleteTeacherHabilitationShouldReturnSuccessResponse()
        {
            // Arrange
            var expectedResponse = new TeacherHabilitationResponseModel
            {
                IsSuccess = true,
                Message = "Habilidade removida do professor com sucesso",
                StatusCode = HttpStatusCode.OK
            };

            _teacherHabilitationServicesMock.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            var teacherHabilitationService = _teacherHabilitationServicesMock.Object;

            // Act
            var response = await teacherHabilitationService.DeleteAsync(1);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Habilidade removida do professor com sucesso", response.Message);
        }

        [Fact]
        public async Task DeleteTeacherHabilitationWithInvalidIdShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new TeacherHabilitationResponseModel
            {
                IsSuccess = false,
                Message = "Vínculo de habilidade não encontrado",
                StatusCode = HttpStatusCode.BadRequest
            };

            _teacherHabilitationServicesMock.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            var teacherHabilitationService = _teacherHabilitationServicesMock.Object;

            // Act
            var response = await teacherHabilitationService.DeleteAsync(999);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Vínculo de habilidade não encontrado", response.Message);
        }

        [Fact]
        public async Task GetTeacherHabilitationsShouldReturnSuccessResponse()
        {
            // Arrange
            var expectedResponse = new List<TeacherHabilitationResponseModel>
            {
                new()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK
                }
            };

            _teacherHabilitationServicesMock.Setup(x => x.GetTeacherHabilitationsAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            var teacherHabilitationService = _teacherHabilitationServicesMock.Object;

            // Act
            var response = await teacherHabilitationService.GetTeacherHabilitationsAsync(1);

            // Assert
            Assert.True(response.Count > 0);
            Assert.True(response.First().IsSuccess);
            Assert.Equal(HttpStatusCode.OK, response.First().StatusCode);
        }

        [Fact]
        public async Task GetTeacherHabilitationsWithInvalidTeacherIdShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new List<TeacherHabilitationResponseModel>
            {
                new()
                {
                    IsSuccess = false,
                    Message = "Professor não encontrado",
                    StatusCode = HttpStatusCode.BadRequest
                }
            };

            _teacherHabilitationServicesMock.Setup(x => x.GetTeacherHabilitationsAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            var teacherHabilitationService = _teacherHabilitationServicesMock.Object;

            // Act
            var response = await teacherHabilitationService.GetTeacherHabilitationsAsync(999);

            // Assert
            Assert.True(response.Count > 0);
            Assert.False(response.First().IsSuccess);
            Assert.Equal("Professor não encontrado", response.First().Message);
        }
    }
}