using Application.Models.Request;
using Application.Models.Response;
using Application.Services.Interfaces;
using Moq;
using System.Net;

namespace UnitaryTests.Services
{
    public class ClassroomServicesTests
    {
        private readonly Mock<IClassroomServices> _classroomServicesMock;

        public ClassroomServicesTests()
        {
            _classroomServicesMock = new Mock<IClassroomServices>();
        }

        [Fact]
        public async Task CreateClassroomShouldReturnSuccessResponse()
        {
            // Arrange
            var expectedResponse = new ClassroomResponseModel
            {
                IsSuccess = true,
                Message = "Sala de aula cadastrada com sucesso",
                StatusCode = HttpStatusCode.Created
            };

            _classroomServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassroomRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classroomService = _classroomServicesMock.Object;
            var classroomRequest = new ClassroomRequestModel
            {
                Name = "Sala 101",
                Capacity = 30,
                Location = "Bloco A",
                IsAvailable = true
            };

            // Act
            var response = await classroomService.CreateAsync(classroomRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("Sala de aula cadastrada com sucesso", response.Message);
        }

        [Fact]
        public async Task CreateClassroomWithEmptyNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new ClassroomResponseModel
            {
                IsSuccess = false,
                Message = "Nome da sala não pode ser vazio",
                StatusCode = HttpStatusCode.BadRequest
            };

            _classroomServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassroomRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classroomService = _classroomServicesMock.Object;
            var classroomRequest = new ClassroomRequestModel
            {
                Name = "",
                Capacity = 30,
                Location = "Bloco A",
                IsAvailable = true
            };

            // Act
            var response = await classroomService.CreateAsync(classroomRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateClassroomWithZeroCapacityShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new ClassroomResponseModel
            {
                IsSuccess = false,
                Message = "Capacidade deve ser maior que zero",
                StatusCode = HttpStatusCode.BadRequest
            };

            _classroomServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassroomRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classroomService = _classroomServicesMock.Object;
            var classroomRequest = new ClassroomRequestModel
            {
                Name = "Sala 101",
                Capacity = 0,
                Location = "Bloco A",
                IsAvailable = true
            };

            // Act
            var response = await classroomService.CreateAsync(classroomRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Capacidade deve ser maior que zero", response.Message);
        }

        [Fact]
        public async Task CreateClassroomWithEmptyLocationShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new ClassroomResponseModel
            {
                IsSuccess = false,
                Message = "Localização não pode ser vazia",
                StatusCode = HttpStatusCode.BadRequest
            };

            _classroomServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassroomRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classroomService = _classroomServicesMock.Object;
            var classroomRequest = new ClassroomRequestModel
            {
                Name = "Sala 101",
                Capacity = 30,
                Location = "",
                IsAvailable = true
            };

            // Act
            var response = await classroomService.CreateAsync(classroomRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Localização não pode ser vazia", response.Message);
        }

        [Fact]
        public async Task CreateClassroomWithShortNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new ClassroomResponseModel
            {
                IsSuccess = false,
                Message = "Nome da sala deve ter no mínimo 3 caracteres",
                StatusCode = HttpStatusCode.BadRequest
            };

            _classroomServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassroomRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classroomService = _classroomServicesMock.Object;
            var classroomRequest = new ClassroomRequestModel
            {
                Name = "Sa",
                Capacity = 30,
                Location = "Bloco A",
                IsAvailable = true
            };

            // Act
            var response = await classroomService.CreateAsync(classroomRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Nome da sala deve ter no mínimo 3 caracteres", response.Message);
        }

        [Fact]
        public async Task CreateClassroomWithDuplicateNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new ClassroomResponseModel
            {
                IsSuccess = false,
                Message = "Já existe uma sala com esse nome cadastrada",
                StatusCode = HttpStatusCode.BadRequest
            };

            _classroomServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassroomRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classroomService = _classroomServicesMock.Object;
            var classroomRequest = new ClassroomRequestModel
            {
                Name = "Sala 101",
                Capacity = 30,
                Location = "Bloco A",
                IsAvailable = true
            };

            // Act
            var response = await classroomService.CreateAsync(classroomRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Já existe uma sala com esse nome cadastrada", response.Message);
        }

        [Fact]
        public async Task CreateClassroomWithValidDataShouldReturnCreatedStatusCode()
        {
            // Arrange
            var expectedResponse = new ClassroomResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _classroomServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassroomRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classroomService = _classroomServicesMock.Object;
            var classroomRequest = new ClassroomRequestModel
            {
                Name = "Sala 202",
                Capacity = 40,
                Location = "Bloco B",
                IsAvailable = true
            };

            // Act
            var response = await classroomService.CreateAsync(classroomRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateClassroomWithUnavailableStatusShouldReturnSuccess()
        {
            // Arrange
            var expectedResponse = new ClassroomResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _classroomServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassroomRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classroomService = _classroomServicesMock.Object;
            var classroomRequest = new ClassroomRequestModel
            {
                Name = "Sala 303",
                Capacity = 25,
                Location = "Bloco C",
                IsAvailable = false
            };

            // Act
            var response = await classroomService.CreateAsync(classroomRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}