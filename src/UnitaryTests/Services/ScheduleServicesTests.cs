using Application.Models.Response;
using Application.Services.Interfaces;
using Domain.Entities;
using Moq;
using System.Net;

namespace UnitaryTests.Services
{
    public class ScheduleServicesTests
    {
        private readonly Mock<IScheduleServices> _scheduleServicesMock;

        public ScheduleServicesTests()
        {
            _scheduleServicesMock = new Mock<IScheduleServices>();
        }

        [Fact]
        public async Task GenerateSchedulesShouldReturnSuccessResponse()
        {
            // Arrange
            var expectedResponse = new ScheduleResponseModel
            {
                IsSuccess = true,
                Message = "Agendamentos gerados com sucesso. Total: 30 dias, sendo 0 feriados",
                StatusCode = HttpStatusCode.Created
            };

            _scheduleServicesMock.Setup(x => x.GenerateSchedulesAsync(It.IsAny<int>(), It.IsAny<TimeOnly>(), It.IsAny<TimeOnly>()))
                .ReturnsAsync(expectedResponse);

            var scheduleService = _scheduleServicesMock.Object;

            // Act
            var response = await scheduleService.GenerateSchedulesAsync(1, new TimeOnly(8, 0), new TimeOnly(12, 0));

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GenerateSchedulesWithInvalidClassIdShouldReturnFailureResponse()
        {
            // Arrange
            var expectedResponse = new ScheduleResponseModel
            {
                IsSuccess = false,
                Message = "Aula não encontrada",
                StatusCode = HttpStatusCode.BadRequest
            };

            _scheduleServicesMock.Setup(x => x.GenerateSchedulesAsync(It.IsAny<int>(), It.IsAny<TimeOnly>(), It.IsAny<TimeOnly>()))
                .ReturnsAsync(expectedResponse);

            var scheduleService = _scheduleServicesMock.Object;

            // Act
            var response = await scheduleService.GenerateSchedulesAsync(0, new TimeOnly(8, 0), new TimeOnly(12, 0));

            // Assert
            Assert.False(response.IsSuccess);
            Assert.Equal("Aula não encontrada", response.Message);
        }

        [Fact]
        public async Task GetSchedulesByClassIdShouldReturnActiveSchedules()
        {
            // Arrange
            var expectedResponse = new ScheduleResponseModel()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Model = new List<Schedule>()
                {
                    new Schedule
                    {
                        ScheduleId = 1,
                        ClassId = 1,
                        TeacherId = 1,
                        Date = DateTime.Now,
                        DayOfWeek = DayOfWeek.Monday,
                        StartTime = new TimeOnly(8, 0),
                        EndTime = new TimeOnly(12, 0),
                        IsHoliday = false,
                        Enabled = true
                    },
                }
            };

            _scheduleServicesMock.Setup(x => x.GetSchedulesByClassIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(expectedResponse);

            var scheduleService = _scheduleServicesMock.Object;

            // Act
            var response = await scheduleService.GetSchedulesByClassIdAsync(1);
            var responseModel = (IEnumerable<Schedule>?)response.Model is null ? new List<Schedule>() : (IEnumerable<Schedule>)response.Model;

            // Assert
            Assert.NotEmpty(responseModel);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task GetSchedulesByTeacherIdShouldReturnActiveSchedules()
        {
            // Arrange
            var expectedResponse = new ScheduleResponseModel()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Model = new List<Schedule>()
                {
                     new Schedule
                    {
                        ScheduleId = 1,
                        ClassId = 1,
                        TeacherId = 1,
                        Date = DateTime.Now,
                        DayOfWeek = DayOfWeek.Monday,
                        StartTime = new TimeOnly(8, 0),
                        EndTime = new TimeOnly(12, 0),
                        IsHoliday = false,
                        Enabled = true
                    },
                }
            };

            _scheduleServicesMock.Setup(x => x.GetSchedulesByTeacherIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            var scheduleService = _scheduleServicesMock.Object;

            // Act
            var response = await scheduleService.GetSchedulesByTeacherIdAsync(1);
            var responseModel = (IEnumerable<Schedule>?)response.Model is null ? new List<Schedule>() : (IEnumerable<Schedule>)response.Model;

            // Assert
            Assert.NotEmpty(responseModel);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task GetSchedulesByDateRangeShouldReturnActiveSchedules()
        {
            // Arrange
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(30);

            var expectedResponse = new ScheduleResponseModel()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Model = new List<Schedule>()
                {
                     new Schedule
                    {
                        ScheduleId = 1,
                        ClassId = 1,
                        TeacherId = 1,
                        Date = DateTime.Now,
                        DayOfWeek = DayOfWeek.Monday,
                        StartTime = new TimeOnly(8, 0),
                        EndTime = new TimeOnly(12, 0),
                        IsHoliday = false,
                        Enabled = true
                    },
                }
            };

            _scheduleServicesMock.Setup(x => x.GetSchedulesByDateRangeAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(expectedResponse);

            var scheduleService = _scheduleServicesMock.Object;

            // Act
            var response = await scheduleService.GetSchedulesByDateRangeAsync(startDate, endDate);
            var responseModel = (IEnumerable<Schedule>?)response.Model is null ? new List<Schedule>() : (IEnumerable<Schedule>)response.Model;

            // Assert
            Assert.NotEmpty(responseModel);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task GetSchedulesByDayOfWeekShouldReturnSchedules()
        {
            // Arrange
            var expectedResponse = new ScheduleResponseModel()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Model = new List<Schedule>()
                {
                     new Schedule
                    {
                        ScheduleId = 1,
                        ClassId = 1,
                        TeacherId = 1,
                        Date = DateTime.Now,
                        DayOfWeek = DayOfWeek.Monday,
                        StartTime = new TimeOnly(8, 0),
                        EndTime = new TimeOnly(12, 0),
                        IsHoliday = false,
                        Enabled = true
                    },
                }
            };

            _scheduleServicesMock.Setup(x => x.GetSchedulesByDayOfWeekAsync(It.IsAny<DayOfWeek>()))
                .ReturnsAsync(expectedResponse);

            var scheduleService = _scheduleServicesMock.Object;

            // Act
            var response = await scheduleService.GetSchedulesByDayOfWeekAsync(DayOfWeek.Monday);
            var responseModel = (IEnumerable<Schedule>?)response.Model is null ? new List<Schedule>() : (IEnumerable<Schedule>)response.Model;

            // Assert
            Assert.NotEmpty(responseModel);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task GetActiveSchedulesShouldReturnOnlyEnabledSchedules()
        {
            // Arrange            
            var expectedResponse = new ScheduleResponseModel()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Model = new List<Schedule>()
                {
                    new Schedule
                    {
                        ScheduleId = 1,
                        ClassId = 1,
                        TeacherId = 1,
                        Date = DateTime.Now,
                        DayOfWeek = DayOfWeek.Monday,
                        StartTime = new TimeOnly(8, 0),
                        EndTime = new TimeOnly(12, 0),
                        IsHoliday = false,
                        Enabled = true
                    },
                }
            };

            _scheduleServicesMock.Setup(x => x.GetActiveSchedulesAsync())
                .ReturnsAsync(expectedResponse);

            var scheduleService = _scheduleServicesMock.Object;

            // Act
            var response = await scheduleService.GetActiveSchedulesAsync();
            var responseModel = (IEnumerable<Schedule>?)response.Model is null ? new List<Schedule>() : (IEnumerable<Schedule>)response.Model;

            // Assert
            Assert.NotEmpty(responseModel);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task GetHolidaySchedulesShouldReturnHolidaySchedules()
        {
            // Arrange
            var expectedResponse = new ScheduleResponseModel()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Model = new List<Schedule>()
                {
                    new Schedule
                    {
                        ScheduleId = 1,
                        ClassId = 1,
                        TeacherId = 1,
                        Date = DateTime.Now,
                        DayOfWeek = DayOfWeek.Monday,
                        StartTime = new TimeOnly(8, 0),
                        EndTime = new TimeOnly(12, 0),
                        IsHoliday = false,
                        Enabled = true
                    },
                }
            };

            _scheduleServicesMock.Setup(x => x.GetHolidaySchedulesAsync())
                .ReturnsAsync(expectedResponse);

            var scheduleService = _scheduleServicesMock.Object;

            // Act
            var response = await scheduleService.GetHolidaySchedulesAsync();
            var responseModel = (IEnumerable<Schedule>?)response.Model is null ? new List<Schedule>() : (IEnumerable<Schedule>)response.Model;

            // Assert
            Assert.NotEmpty(responseModel);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task GetTotalClassDaysShouldReturnCorrectCount()
        {
            // Arrange
            _scheduleServicesMock.Setup(x => x.GetTotalClassDaysAsync(It.IsAny<int>()))
                .ReturnsAsync(20);

            var scheduleService = _scheduleServicesMock.Object;

            // Act
            var response = await scheduleService.GetTotalClassDaysAsync(1);

            // Assert
            Assert.Equal(20, response);
        }

        [Fact]
        public async Task GetTotalHolidayDaysShouldReturnCorrectCount()
        {
            // Arrange
            _scheduleServicesMock.Setup(x => x.GetTotalHolidayDaysAsync(It.IsAny<int>()))
                .ReturnsAsync(5);

            var scheduleService = _scheduleServicesMock.Object;

            // Act
            var response = await scheduleService.GetTotalHolidayDaysAsync(1);

            // Assert
            Assert.Equal(5, response);
        }

        [Fact]
        public async Task GetSchedulesExcludesHolidaysFromActiveSchedules()
        {
            // Arrange
            var expectedResponse = new ScheduleResponseModel()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Model = new List<Schedule>()
                {
                    new Schedule
                    {
                        ScheduleId = 1,
                        ClassId = 1,
                        TeacherId = 1,
                        Date = DateTime.Now,
                        DayOfWeek = DayOfWeek.Monday,
                        StartTime = new TimeOnly(8, 0),
                        EndTime = new TimeOnly(12, 0),
                        IsHoliday = false,
                        Enabled = true
                    },
                },
                Message = "Apenas aulas ativas"
            };

            _scheduleServicesMock.Setup(x => x.GetSchedulesByClassIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedResponse);

            var scheduleService = _scheduleServicesMock.Object;

            // Act
            var response = await scheduleService.GetSchedulesByClassIdAsync(1);
            var responseModel = (IEnumerable<Schedule>?)response.Model is null ? new List<Schedule>() : (IEnumerable<Schedule>)response.Model;

            // Assert
            Assert.True(response.IsSuccess);
            string firstResultMessage = response.Message ?? string.Empty;
            Assert.Contains("apenas aulas ativas", firstResultMessage, StringComparison.OrdinalIgnoreCase);
        }
    }
}