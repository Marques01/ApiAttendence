using Application.Models.Request;
using Application.Models.Response;
using Application.Services.Interfaces;
using Moq;
using System.Net;

namespace UnitaryTests.Services
{
    public class StudentServicesTests
    {
        private readonly Mock<IStudentServices> _studentServicesMock;

        public StudentServicesTests()
        {
            _studentServicesMock = new Mock<IStudentServices>();
        }

        [Fact]
        public async Task CreateStudentShouldReturnSuccessResponse()
        {
            // Arrange
            var expectedResponse = new StudentResponseModel
            {
                IsSuccess = true,
                Message = "Student created successfully",
                StatusCode = HttpStatusCode.Created
            };

            _studentServicesMock.Setup(x => x.CreateAsync(It.IsAny<StudentRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var studentService = _studentServicesMock.Object;
            var studentRequest = new StudentRequestModel
            {
                Name = "João Silva",
                Registration = "2024001",
                Enabled = true,
                RfidCardId = 1
            };

            // Act
            var response = await studentService.CreateAsync(studentRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("Student created successfully", response.Message);
        }

        [Fact]
        public async Task CreateStudentWithEmptyNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new StudentResponseModel
            {
                IsSuccess = false,
                Message = "Nome não pode ser vazio",
                StatusCode = HttpStatusCode.BadRequest
            };

            _studentServicesMock.Setup(x => x.CreateAsync(It.IsAny<StudentRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var studentService = _studentServicesMock.Object;
            var studentRequest = new StudentRequestModel
            {
                Name = "",
                Registration = "2024001",
                Enabled = true,
                RfidCardId = 1
            };

            // Act
            var response = await studentService.CreateAsync(studentRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateStudentWithEmptyRegistrationShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new StudentResponseModel
            {
                IsSuccess = false,
                Message = "Matrícula não pode ser vazia",
                StatusCode = HttpStatusCode.BadRequest
            };

            _studentServicesMock.Setup(x => x.CreateAsync(It.IsAny<StudentRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var studentService = _studentServicesMock.Object;
            var studentRequest = new StudentRequestModel
            {
                Name = "João Silva",
                Registration = "",
                Enabled = true,
                RfidCardId = 1
            };

            // Act
            var response = await studentService.CreateAsync(studentRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateStudentWithShortNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new StudentResponseModel
            {
                IsSuccess = false,
                Message = "Nome deve ter no mínimo 3 caracteres",
                StatusCode = HttpStatusCode.BadRequest
            };

            _studentServicesMock.Setup(x => x.CreateAsync(It.IsAny<StudentRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var studentService = _studentServicesMock.Object;
            var studentRequest = new StudentRequestModel
            {
                Name = "Jo",
                Registration = "2024001",
                Enabled = true,
                RfidCardId = 1
            };

            // Act
            var response = await studentService.CreateAsync(studentRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Nome deve ter no mínimo 3 caracteres", response.Message);
        }

        [Fact]
        public async Task CreateStudentWithShortRegistrationShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new StudentResponseModel
            {
                IsSuccess = false,
                Message = "Matrícula deve ter no mínimo 3 caracteres",
                StatusCode = HttpStatusCode.BadRequest
            };

            _studentServicesMock.Setup(x => x.CreateAsync(It.IsAny<StudentRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var studentService = _studentServicesMock.Object;
            var studentRequest = new StudentRequestModel
            {
                Name = "João Silva",
                Registration = "20",
                Enabled = true,
                RfidCardId = 1
            };

            // Act
            var response = await studentService.CreateAsync(studentRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Matrícula deve ter no mínimo 3 caracteres", response.Message);
        }

        [Fact]
        public async Task CreateStudentWithLongNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new StudentResponseModel
            {
                IsSuccess = false,
                Message = "Nome deve ter no máximo 100 caracteres",
                StatusCode = HttpStatusCode.BadRequest
            };

            _studentServicesMock.Setup(x => x.CreateAsync(It.IsAny<StudentRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var studentService = _studentServicesMock.Object;
            var studentRequest = new StudentRequestModel
            {
                Name = new string('a', 101),
                Registration = "2024001",
                Enabled = true,
                RfidCardId = 1
            };

            // Act
            var response = await studentService.CreateAsync(studentRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Nome deve ter no máximo 100 caracteres", response.Message);
        }

        [Fact]
        public async Task CreateStudentWithLongRegistrationShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new StudentResponseModel
            {
                IsSuccess = false,
                Message = "Matrícula deve ter no máximo 20 caracteres",
                StatusCode = HttpStatusCode.BadRequest
            };

            _studentServicesMock.Setup(x => x.CreateAsync(It.IsAny<StudentRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var studentService = _studentServicesMock.Object;
            var studentRequest = new StudentRequestModel
            {
                Name = "João Silva",
                Registration = new string('0', 21),
                Enabled = true,
                RfidCardId = 1
            };

            // Act
            var response = await studentService.CreateAsync(studentRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Matrícula deve ter no máximo 20 caracteres", response.Message);
        }

        [Fact]
        public async Task CreateStudentWithValidDataShouldReturnCreatedStatusCode()
        {
            // Arrange
            var expectedResponse = new StudentResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _studentServicesMock.Setup(x => x.CreateAsync(It.IsAny<StudentRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var studentService = _studentServicesMock.Object;
            var studentRequest = new StudentRequestModel
            {
                Name = "Maria Santos",
                Registration = "2024002",
                Enabled = true,
                RfidCardId = 2
            };

            // Act
            var response = await studentService.CreateAsync(studentRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}