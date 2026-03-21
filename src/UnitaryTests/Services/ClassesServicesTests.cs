using Application.Models.Request;
using Application.Models.Response;
using Application.Services.Interfaces;
using Moq;
using System.Net;

namespace UnitaryTests.Services
{
    public class ClassesServicesTests
    {
        private readonly Mock<IClassesServices> _classesServicesMock;

        public ClassesServicesTests()
        {
            _classesServicesMock = new Mock<IClassesServices>();
        }

        [Fact]
        public async Task CreateClassesShouldReturnSuccessResponse()
        {
            // Arrange
            var expectedResponse = new ClassesResponseModel
            {
                IsSuccess = true,
                Message = "Aula cadastrada com sucesso",
                StatusCode = HttpStatusCode.Created
            };

            _classesServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassesRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classesService = _classesServicesMock.Object;
            var classesRequest = new ClassesRequestModel
            {
                Name = "Matemática Avançada",
                TeacherId = 1,
                ClassroomId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                Notes = "Aula de matemática avançada",
                Enabled = true
            };

            // Act
            var response = await classesService.CreateAsync(classesRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("Aula cadastrada com sucesso", response.Message);
        }

        [Fact]
        public async Task CreateClassesWithEmptyNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new ClassesResponseModel
            {
                IsSuccess = false,
                Message = "Nome da aula não pode ser vazio",
                StatusCode = HttpStatusCode.BadRequest
            };

            _classesServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassesRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classesService = _classesServicesMock.Object;
            var classesRequest = new ClassesRequestModel
            {
                Name = "",
                TeacherId = 1,
                ClassroomId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                Enabled = true
            };

            // Act
            var response = await classesService.CreateAsync(classesRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateClassesWithInvalidTeacherIdShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new ClassesResponseModel
            {
                IsSuccess = false,
                Message = "Professor não encontrado",
                StatusCode = HttpStatusCode.BadRequest
            };

            _classesServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassesRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classesService = _classesServicesMock.Object;
            var classesRequest = new ClassesRequestModel
            {
                Name = "Matemática Avançada",
                TeacherId = 0,
                ClassroomId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                Enabled = true
            };

            // Act
            var response = await classesService.CreateAsync(classesRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Professor não encontrado", response.Message);
        }

        [Fact]
        public async Task CreateClassesWithInvalidClassroomIdShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new ClassesResponseModel
            {
                IsSuccess = false,
                Message = "Sala de aula não encontrada",
                StatusCode = HttpStatusCode.BadRequest
            };

            _classesServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassesRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classesService = _classesServicesMock.Object;
            var classesRequest = new ClassesRequestModel
            {
                Name = "Matemática Avançada",
                TeacherId = 1,
                ClassroomId = 0,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                Enabled = true
            };

            // Act
            var response = await classesService.CreateAsync(classesRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Sala de aula não encontrada", response.Message);
        }

        [Fact]
        public async Task CreateClassesWithShortNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new ClassesResponseModel
            {
                IsSuccess = false,
                Message = "Nome da aula deve ter no mínimo 3 caracteres",
                StatusCode = HttpStatusCode.BadRequest
            };

            _classesServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassesRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classesService = _classesServicesMock.Object;
            var classesRequest = new ClassesRequestModel
            {
                Name = "Ma",
                TeacherId = 1,
                ClassroomId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                Enabled = true
            };

            // Act
            var response = await classesService.CreateAsync(classesRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Nome da aula deve ter no mínimo 3 caracteres", response.Message);
        }

        [Fact]
        public async Task CreateClassesWithInvalidDateRangeShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new ClassesResponseModel
            {
                IsSuccess = false,
                Message = "Data de início deve ser anterior à data de término",
                StatusCode = HttpStatusCode.BadRequest
            };

            _classesServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassesRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classesService = _classesServicesMock.Object;
            var classesRequest = new ClassesRequestModel
            {
                Name = "Matemática Avançada",
                TeacherId = 1,
                ClassroomId = 1,
                StartDate = DateTime.Now.AddDays(30),
                EndDate = DateTime.Now,
                Enabled = true
            };

            // Act
            var response = await classesService.CreateAsync(classesRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Data de início deve ser anterior à data de término", response.Message);
        }

        [Fact]
        public async Task CreateClassesWithValidDataShouldReturnCreatedStatusCode()
        {
            // Arrange
            var expectedResponse = new ClassesResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _classesServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassesRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classesService = _classesServicesMock.Object;
            var classesRequest = new ClassesRequestModel
            {
                Name = "Física Quântica",
                TeacherId = 2,
                ClassroomId = 2,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(60),
                Notes = "Aula avançada de física",
                Enabled = true
            };

            // Act
            var response = await classesService.CreateAsync(classesRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateClassesWithDisabledStatusShouldReturnSuccess()
        {
            // Arrange
            var expectedResponse = new ClassesResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _classesServicesMock.Setup(x => x.CreateAsync(It.IsAny<ClassesRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var classesService = _classesServicesMock.Object;
            var classesRequest = new ClassesRequestModel
            {
                Name = "Português Clássico",
                TeacherId = 3,
                ClassroomId = 3,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                Enabled = false
            };

            // Act
            var response = await classesService.CreateAsync(classesRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}