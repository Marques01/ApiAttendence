using Application.Models.Request;
using Application.Models.Response;
using Application.Services.Interfaces;
using Moq;
using System.Net;

namespace UnitaryTests.Services
{
    public class HolidayServicesTests
    {
        private readonly Mock<IHolidayServices> _holidayServicesMock;

        public HolidayServicesTests()
        {
            _holidayServicesMock = new Mock<IHolidayServices>();
        }

        [Fact]
        public async Task CreateHolidayShouldReturnSuccessResponse()
        {
            // Arrange
            var expectedResponse = new HolidayResponseModel
            {
                IsSuccess = true,
                Message = "Feriado cadastrado com sucesso",
                StatusCode = HttpStatusCode.Created
            };

            _holidayServicesMock.Setup(x => x.CreateAsync(It.IsAny<HolidayRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var holidayService = _holidayServicesMock.Object;
            var holidayRequest = new HolidayRequestModel
            {
                Name = "Natal",
                Day = new DateTime(2024, 12, 25),
                IsFixedDate = true,
                National = true
            };

            // Act
            var response = await holidayService.CreateAsync(holidayRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("Feriado cadastrado com sucesso", response.Message);
        }

        [Fact]
        public async Task CreateHolidayWithEmptyNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new HolidayResponseModel
            {
                IsSuccess = false,
                Message = "Nome do feriado não pode ser vazio",
                StatusCode = HttpStatusCode.BadRequest
            };

            _holidayServicesMock.Setup(x => x.CreateAsync(It.IsAny<HolidayRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var holidayService = _holidayServicesMock.Object;
            var holidayRequest = new HolidayRequestModel
            {
                Name = "",
                Day = new DateTime(2024, 12, 25),
                IsFixedDate = true,
                National = true
            };

            // Act
            var response = await holidayService.CreateAsync(holidayRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHolidayWithShortNameShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new HolidayResponseModel
            {
                IsSuccess = false,
                Message = "Nome do feriado deve ter no mínimo 3 caracteres",
                StatusCode = HttpStatusCode.BadRequest
            };

            _holidayServicesMock.Setup(x => x.CreateAsync(It.IsAny<HolidayRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var holidayService = _holidayServicesMock.Object;
            var holidayRequest = new HolidayRequestModel
            {
                Name = "Na",
                Day = new DateTime(2024, 12, 25),
                IsFixedDate = true,
                National = true
            };

            // Act
            var response = await holidayService.CreateAsync(holidayRequest);

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Nome do feriado deve ter no mínimo 3 caracteres", response.Message);
        }

        [Fact]
        public async Task CreateHolidayWithFixedDateShouldReturnSuccess()
        {
            // Arrange
            var expectedResponse = new HolidayResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _holidayServicesMock.Setup(x => x.CreateAsync(It.IsAny<HolidayRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var holidayService = _holidayServicesMock.Object;
            var holidayRequest = new HolidayRequestModel
            {
                Name = "Ano Novo",
                Day = new DateTime(2024, 1, 1),
                IsFixedDate = true,
                National = true
            };

            // Act
            var response = await holidayService.CreateAsync(holidayRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateHolidayWithMoveableDateShouldReturnSuccess()
        {
            // Arrange
            var expectedResponse = new HolidayResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _holidayServicesMock.Setup(x => x.CreateAsync(It.IsAny<HolidayRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var holidayService = _holidayServicesMock.Object;
            var holidayRequest = new HolidayRequestModel
            {
                Name = "Páscoa",
                Day = new DateTime(2024, 3, 31),
                IsFixedDate = false,
                National = false
            };

            // Act
            var response = await holidayService.CreateAsync(holidayRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateNonNationalHolidayShouldReturnSuccess()
        {
            // Arrange
            var expectedResponse = new HolidayResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _holidayServicesMock.Setup(x => x.CreateAsync(It.IsAny<HolidayRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var holidayService = _holidayServicesMock.Object;
            var holidayRequest = new HolidayRequestModel
            {
                Name = "Recesso Escolar",
                Day = new DateTime(2024, 7, 15),
                IsFixedDate = false,
                National = false
            };

            // Act
            var response = await holidayService.CreateAsync(holidayRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateHolidayWithValidDataShouldReturnCreatedStatusCode()
        {
            // Arrange
            var expectedResponse = new HolidayResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created
            };

            _holidayServicesMock.Setup(x => x.CreateAsync(It.IsAny<HolidayRequestModel>()))
                .ReturnsAsync(expectedResponse);

            var holidayService = _holidayServicesMock.Object;
            var holidayRequest = new HolidayRequestModel
            {
                Name = "Corpus Christi",
                Day = new DateTime(2024, 5, 30),
                IsFixedDate = false,
                National = true
            };

            // Act
            var response = await holidayService.CreateAsync(holidayRequest);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}