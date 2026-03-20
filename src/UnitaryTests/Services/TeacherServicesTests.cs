using Application.Models.Request;
using Application.Models.Response;
using Application.Services.Interfaces;
using Moq;
using System.Net;

namespace UnitaryTests.Services
{
    public class TeacherServicesTests
    {
        private readonly Mock<ITeacherServices> _teacherServicesMock;

        public TeacherServicesTests()
        {
            _teacherServicesMock = new Mock<ITeacherServices>();
        }

        [Fact]
        public async Task CreateTeacherShouldReturnSuccessResponse()
        {
            // Arrange
            var expectedResponse = new TeacherResponseModel
            {
                IsSuccess = true,
                Message = "Teacher created successfully",
                StatusCode = HttpStatusCode.Created
            };

            _teacherServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherService = _teacherServicesMock.Object;
            var teacherRequest = new TeacherRequestModel
            {
                Name = "Prof. João Silva",
                Registration = "DOC001",
                Email = "joao.silva@escola.com",
                Enabled = true
            };

            // Act
            var response = await teacherService.CreateAsync(teacherRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("Teacher created successfully", response.Message);
        }

        [Fact]
        public async Task CreateTeacherWithEmptyNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new TeacherResponseModel
            {
                IsSuccess = false,
                Message = "Nome não pode ser vazio",
                StatusCode = HttpStatusCode.BadRequest
            };

            _teacherServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherService = _teacherServicesMock.Object;
            var teacherRequest = new TeacherRequestModel
            {
                Name = "",
                Registration = "DOC001",
                Email = "joao.silva@escola.com",
                Enabled = true
            };

            // Act
            var response = await teacherService.CreateAsync(teacherRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTeacherWithEmptyRegistrationShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new TeacherResponseModel
            {
                IsSuccess = false,
                Message = "Matrícula não pode ser vazia",
                StatusCode = HttpStatusCode.BadRequest
            };

            _teacherServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherService = _teacherServicesMock.Object;
            var teacherRequest = new TeacherRequestModel
            {
                Name = "Prof. João Silva",
                Registration = "",
                Email = "joao.silva@escola.com",
                Enabled = true
            };

            // Act
            var response = await teacherService.CreateAsync(teacherRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTeacherWithEmptyEmailShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new TeacherResponseModel
            {
                IsSuccess = false,
                Message = "Email não pode ser vazio",
                StatusCode = HttpStatusCode.BadRequest
            };

            _teacherServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherService = _teacherServicesMock.Object;
            var teacherRequest = new TeacherRequestModel
            {
                Name = "Prof. João Silva",
                Registration = "DOC001",
                Email = "",
                Enabled = true
            };

            // Act
            var response = await teacherService.CreateAsync(teacherRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTeacherWithInvalidEmailShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new TeacherResponseModel
            {
                IsSuccess = false,
                Message = "Email inválido",
                StatusCode = HttpStatusCode.BadRequest
            };

            _teacherServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherService = _teacherServicesMock.Object;
            var teacherRequest = new TeacherRequestModel
            {
                Name = "Prof. João Silva",
                Registration = "DOC001",
                Email = "email_invalido",
                Enabled = true
            };

            // Act
            var response = await teacherService.CreateAsync(teacherRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Email inválido", response.Message);
        }

        [Fact]
        public async Task CreateTeacherWithShortNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new TeacherResponseModel
            {
                IsSuccess = false,
                Message = "Nome deve ter no mínimo 3 caracteres",
                StatusCode = HttpStatusCode.BadRequest
            };

            _teacherServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherService = _teacherServicesMock.Object;
            var teacherRequest = new TeacherRequestModel
            {
                Name = "Jo",
                Registration = "DOC001",
                Email = "joao.silva@escola.com",
                Enabled = true
            };

            // Act
            var response = await teacherService.CreateAsync(teacherRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Nome deve ter no mínimo 3 caracteres", response.Message);
        }

        [Fact]
        public async Task CreateTeacherWithShortRegistrationShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new TeacherResponseModel
            {
                IsSuccess = false,
                Message = "Matrícula deve ter no mínimo 3 caracteres",
                StatusCode = HttpStatusCode.BadRequest
            };

            _teacherServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherService = _teacherServicesMock.Object;
            var teacherRequest = new TeacherRequestModel
            {
                Name = "Prof. João Silva",
                Registration = "DO",
                Email = "joao.silva@escola.com",
                Enabled = true
            };

            // Act
            var response = await teacherService.CreateAsync(teacherRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Matrícula deve ter no mínimo 3 caracteres", response.Message);
        }

        [Fact]
        public async Task CreateTeacherWithLongNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new TeacherResponseModel
            {
                IsSuccess = false,
                Message = "Nome deve ter no máximo 100 caracteres",
                StatusCode = HttpStatusCode.BadRequest
            };

            _teacherServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherService = _teacherServicesMock.Object;
            var teacherRequest = new TeacherRequestModel
            {
                Name = new string('a', 101),
                Registration = "DOC001",
                Email = "joao.silva@escola.com",
                Enabled = true
            };

            // Act
            var response = await teacherService.CreateAsync(teacherRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Nome deve ter no máximo 100 caracteres", response.Message);
        }

        [Fact]
        public async Task CreateTeacherWithLongRegistrationShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new TeacherResponseModel
            {
                IsSuccess = false,
                Message = "Matrícula deve ter no máximo 20 caracteres",
                StatusCode = HttpStatusCode.BadRequest
            };

            _teacherServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherService = _teacherServicesMock.Object;
            var teacherRequest = new TeacherRequestModel
            {
                Name = "Prof. João Silva",
                Registration = new string('0', 21),
                Email = "joao.silva@escola.com",
                Enabled = true
            };

            // Act
            var response = await teacherService.CreateAsync(teacherRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Matrícula deve ter no máximo 20 caracteres", response.Message);
        }

        [Fact]
        public async Task CreateTeacherWithValidDataShouldReturnCreatedStatusCode()
        {
            // Arrange
            var expectedResponse = new TeacherResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _teacherServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherService = _teacherServicesMock.Object;
            var teacherRequest = new TeacherRequestModel
            {
                Name = "Prof. Maria Santos",
                Registration = "DOC002",
                Email = "maria.santos@escola.com",
                Enabled = true
            };

            // Act
            var response = await teacherService.CreateAsync(teacherRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateTeacherWithDisabledStatusShouldReturnSuccess()
        {
            // Arrange
            var expectedResponse = new TeacherResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _teacherServicesMock.Setup(x => x.CreateAsync(It.IsAny<TeacherRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var teacherService = _teacherServicesMock.Object;
            var teacherRequest = new TeacherRequestModel
            {
                Name = "Prof. Pedro Costa",
                Registration = "DOC003",
                Email = "pedro.costa@escola.com",
                Enabled = false
            };

            // Act
            var response = await teacherService.CreateAsync(teacherRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}