using Application.Models.Request;
using Application.Models.Response;
using Application.Services.Interfaces;
using Moq;
using System.Net;

namespace UnitaryTests.Services
{
    public class HabilitationServicesTests
    {
        private readonly Mock<IHabilitationServices> _habilitationServicesMock;

        public HabilitationServicesTests()
        {
            _habilitationServicesMock = new Mock<IHabilitationServices>();
        }

        [Fact]
        public async Task CreateHabilitationShouldReturnSuccessResponse()
        {
            // Arrange
            var expectedResponse = new HabilitationResponseModel
            {
                IsSuccess = true,
                Message = "Habilitation created successfully",
                StatusCode = HttpStatusCode.Created
            };

            _habilitationServicesMock.Setup(x => x.CreateAsync(It.IsAny<HabilitationRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var habilitationService = _habilitationServicesMock.Object;
            var habilitationRequest = new HabilitationRequestModel
            {
                Name = "Matemática",
                Description = "Ensino de Matemática avançada",
                Enabled = true
            };

            // Act
            var response = await habilitationService.CreateAsync(habilitationRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("Habilitation created successfully", response.Message);
        }

        [Fact]
        public async Task CreateHabilitationWithEmptyNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new HabilitationResponseModel
            {
                IsSuccess = false,
                Message = "Nome da habilidade não pode ser vazio",
                StatusCode = HttpStatusCode.BadRequest
            };

            _habilitationServicesMock.Setup(x => x.CreateAsync(It.IsAny<HabilitationRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var habilitationService = _habilitationServicesMock.Object;
            var habilitationRequest = new HabilitationRequestModel
            {
                Name = "",
                Description = "Ensino de Matemática avançada",
                Enabled = true
            };

            // Act
            var response = await habilitationService.CreateAsync(habilitationRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHabilitationWithEmptyDescriptionShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new HabilitationResponseModel
            {
                IsSuccess = false,
                Message = "Descrição não pode ser vazia",
                StatusCode = HttpStatusCode.BadRequest
            };

            _habilitationServicesMock.Setup(x => x.CreateAsync(It.IsAny<HabilitationRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var habilitationService = _habilitationServicesMock.Object;
            var habilitationRequest = new HabilitationRequestModel
            {
                Name = "Matemática",
                Description = "",
                Enabled = true
            };

            // Act
            var response = await habilitationService.CreateAsync(habilitationRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHabilitationWithShortNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new HabilitationResponseModel
            {
                IsSuccess = false,
                Message = "Nome da habilidade deve ter no mínimo 3 caracteres",
                StatusCode = HttpStatusCode.BadRequest
            };

            _habilitationServicesMock.Setup(x => x.CreateAsync(It.IsAny<HabilitationRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var habilitationService = _habilitationServicesMock.Object;
            var habilitationRequest = new HabilitationRequestModel
            {
                Name = "Ma",
                Description = "Ensino de Matemática avançada",
                Enabled = true
            };

            // Act
            var response = await habilitationService.CreateAsync(habilitationRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Nome da habilidade deve ter no mínimo 3 caracteres", response.Message);
        }

        [Fact]
        public async Task CreateHabilitationWithShortDescriptionShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new HabilitationResponseModel
            {
                IsSuccess = false,
                Message = "Descrição deve ter no mínimo 5 caracteres",
                StatusCode = HttpStatusCode.BadRequest
            };

            _habilitationServicesMock.Setup(x => x.CreateAsync(It.IsAny<HabilitationRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var habilitationService = _habilitationServicesMock.Object;
            var habilitationRequest = new HabilitationRequestModel
            {
                Name = "Matemática",
                Description = "Test",
                Enabled = true
            };

            // Act
            var response = await habilitationService.CreateAsync(habilitationRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Descrição deve ter no mínimo 5 caracteres", response.Message);
        }

        [Fact]
        public async Task CreateHabilitationWithLongNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new HabilitationResponseModel
            {
                IsSuccess = false,
                Message = "Nome da habilidade deve ter no máximo 100 caracteres",
                StatusCode = HttpStatusCode.BadRequest
            };

            _habilitationServicesMock.Setup(x => x.CreateAsync(It.IsAny<HabilitationRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var habilitationService = _habilitationServicesMock.Object;
            var habilitationRequest = new HabilitationRequestModel
            {
                Name = new string('a', 101),
                Description = "Ensino de Matemática avançada",
                Enabled = true
            };

            // Act
            var response = await habilitationService.CreateAsync(habilitationRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Nome da habilidade deve ter no máximo 100 caracteres", response.Message);
        }

        [Fact]
        public async Task CreateHabilitationWithLongDescriptionShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new HabilitationResponseModel
            {
                IsSuccess = false,
                Message = "Descrição deve ter no máximo 500 caracteres",
                StatusCode = HttpStatusCode.BadRequest
            };

            _habilitationServicesMock.Setup(x => x.CreateAsync(It.IsAny<HabilitationRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var habilitationService = _habilitationServicesMock.Object;
            var habilitationRequest = new HabilitationRequestModel
            {
                Name = "Matemática",
                Description = new string('a', 501),
                Enabled = true
            };

            // Act
            var response = await habilitationService.CreateAsync(habilitationRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Descrição deve ter no máximo 500 caracteres", response.Message);
        }

        [Fact]
        public async Task CreateHabilitationWithValidDataShouldReturnCreatedStatusCode()
        {
            // Arrange
            var expectedResponse = new HabilitationResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _habilitationServicesMock.Setup(x => x.CreateAsync(It.IsAny<HabilitationRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var habilitationService = _habilitationServicesMock.Object;
            var habilitationRequest = new HabilitationRequestModel
            {
                Name = "Português",
                Description = "Ensino de Português avançado",
                Enabled = true
            };

            // Act
            var response = await habilitationService.CreateAsync(habilitationRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateHabilitationWithDisabledStatusShouldReturnSuccess()
        {
            // Arrange
            var expectedResponse = new HabilitationResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _habilitationServicesMock.Setup(x => x.CreateAsync(It.IsAny<HabilitationRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var habilitationService = _habilitationServicesMock.Object;
            var habilitationRequest = new HabilitationRequestModel
            {
                Name = "Física",
                Description = "Ensino de Física avançada",
                Enabled = false
            };

            // Act
            var response = await habilitationService.CreateAsync(habilitationRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}