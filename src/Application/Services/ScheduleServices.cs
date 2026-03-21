using Application.Models.Factories;
using Application.Models.Response;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Repository.Interfaces;
using System.Net;

namespace Application.Services
{
    public class ScheduleServices : IScheduleServices
    {
        private readonly IUnitOfWork _uof;

        public ScheduleServices(IUnitOfWork uof)
        {
            _uof = uof;
        }

        /// <summary>
        /// Gera automaticamente os agendamentos para uma classe, excluindo feriados
        /// </summary>
        public async Task<ScheduleResponseModel> GenerateSchedulesAsync(int classId, TimeOnly startTime, TimeOnly endTime)
        {
            try
            {
                var classes = await _uof.ClassesRepository.GetClassesByIdAsync(false, classId);

                if (classes.ClassId == 0)
                    throw new ArgumentException("Aula não encontrada");

                var holidays = await _uof.HolidayRepository.GetHolidaysByDateRangeAsync(false, classes.StartDate, classes.EndDate);

                var schedulesToCreate = new List<Schedule>();
                var currentDate = classes.StartDate;

                while (currentDate <= classes.EndDate)
                {
                    // Verifica se é feriado
                    var isHoliday = IsHolidayDate(currentDate, holidays);

                    // Cria o agendamento mesmo que seja feriado (para registro)
                    var schedule = ScheduleFactory.CreateSchedule(
                        classId,
                        classes.TeacherId,
                        currentDate,
                        startTime,
                        endTime,
                        isHoliday);

                    schedulesToCreate.Add(schedule);
                    currentDate = currentDate.AddDays(1);
                }

                await _uof.ScheduleRepository.CreateMultipleAsync(schedulesToCreate);
                await _uof.CommitAsync();

                return new ScheduleResponseModel()
                {
                    IsSuccess = true,
                    Message = $"Agendamentos gerados com sucesso. Total: {schedulesToCreate.Count} dias, sendo {schedulesToCreate.Count(s => s.IsHoliday)} feriados",
                    StatusCode = HttpStatusCode.Created,
                    Model = new
                    {
                        TotalDays = schedulesToCreate.Count,
                        ActiveDays = schedulesToCreate.Count(s => !s.IsHoliday),
                        HolidayDays = schedulesToCreate.Count(s => s.IsHoliday),
                        ClassId = classId
                    }
                };
            }
            catch (ArgumentException arg)
            {
                throw new ArgumentException(arg.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna apenas os agendamentos ATIVOS (não feriados) de uma classe
        /// </summary>
        public async Task<ScheduleResponseModel> GetSchedulesByClassIdAsync(int classId)
        {
            try
            {
                var schedules = await _uof.ScheduleRepository.GetSchedulesByClassIdAsync(false, classId);

                var schedulesModel = schedules
                    .Where(s => !s.IsHoliday && s.Enabled) // Apenas aulas ativas                    
                    .ToList();

                return new ScheduleResponseModel()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Model = schedulesModel,
                    Message = $"Total de agendamentos ativos para a classe {classId}: {schedulesModel.Count}"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna apenas os agendamentos ATIVOS de um professor
        /// </summary>
        public async Task<ScheduleResponseModel> GetSchedulesByTeacherIdAsync(int teacherId)
        {
            try
            {
                var schedules = await _uof.ScheduleRepository.GetSchedulesByTeacherIdAsync(false, teacherId);

                var schedulesModel = schedules
                    .Where(s => !s.IsHoliday && s.Enabled)
                    .ToList();

                return new ScheduleResponseModel()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Model = schedulesModel,
                    Message = $"Total de agendamentos ativos para o professor {teacherId}: {schedulesModel.Count}"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna apenas os agendamentos ATIVOS em um período
        /// </summary>
        public async Task<ScheduleResponseModel> GetSchedulesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var schedules = await _uof.ScheduleRepository.GetSchedulesByDateRangeAsync(false, startDate, endDate);

                var schedulesModel = schedules
                    .Where(s => !s.IsHoliday && s.Enabled)                    
                    .ToList();

                return new ScheduleResponseModel()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Model = schedulesModel,
                    Message = $"Total de agendamentos ativos entre {startDate:dd/MM/yyyy} e {endDate:dd/MM/yyyy}: {schedulesModel.Count}"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna todos os agendamentos de um dia da semana específico (apenas ativos)
        /// </summary>
        public async Task<ScheduleResponseModel> GetSchedulesByDayOfWeekAsync(DayOfWeek dayOfWeek)
        {
            try
            {
                var schedules = await _uof.ScheduleRepository.GetSchedulesByDateRangeAsync(false, DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1));

                var schedulesModel = schedules
                    .Where(s => s.DayOfWeek == dayOfWeek && !s.IsHoliday && s.Enabled)
                    .ToList();

                return new ScheduleResponseModel()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Model = schedulesModel,
                    Message = $"Total de agendamentos ativos para o dia da semana {dayOfWeek}: {schedulesModel.Count}"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna todos os agendamentos ativos (não feriados e habilitados)
        /// </summary>
        public async Task<ScheduleResponseModel> GetActiveSchedulesAsync()
        {
            try
            {
                var schedules = await _uof.ScheduleRepository.GetSchedulesByDateRangeAsync(false, DateTime.Now, DateTime.Now.AddYears(1));

                var schedulesModel = schedules
                    .Where(s => !s.IsHoliday && s.Enabled)                   
                    .ToList();

                return new ScheduleResponseModel()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Model = schedulesModel,
                    Message = $"Total de agendamentos ativos: {schedulesModel.Count}"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna todos os agendamentos que caem em feriados
        /// </summary>
        public async Task<ScheduleResponseModel> GetHolidaySchedulesAsync()
        {
            try
            {
                var schedules = await _uof.ScheduleRepository.GetSchedulesByDateRangeAsync(false, DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1));

                var schedulesModel = schedules
                    .Where(s => s.IsHoliday)                    
                    .ToList();

                return new ScheduleResponseModel()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Model = schedulesModel,
                    Message = $"Total de agendamentos em feriados: {schedulesModel.Count}"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna o total de dias de aula (sem feriados) de uma classe
        /// </summary>
        public async Task<int> GetTotalClassDaysAsync(int classId)
        {
            try
            {
                var schedules = await _uof.ScheduleRepository.GetSchedulesByClassIdAsync(false, classId);
                return schedules.Count(s => !s.IsHoliday && s.Enabled);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna o total de feriados de uma classe
        /// </summary>
        public async Task<int> GetTotalHolidayDaysAsync(int classId)
        {
            try
            {
                var schedules = await _uof.ScheduleRepository.GetSchedulesByClassIdAsync(false, classId);
                return schedules.Count(s => s.IsHoliday);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Verifica se uma data é feriado
        /// </summary>
        private bool IsHolidayDate(DateTime date, IEnumerable<Holiday> holidays)
        {
            foreach (var holiday in holidays)
            {
                if (holiday.IsFixedDate)
                {
                    // Para datas fixas, compara mês e dia
                    if (holiday.Day.Month == date.Month && holiday.Day.Day == date.Day)
                        return true;
                }
                else
                {
                    // Para datas móveis, compara a data exata
                    if (holiday.Day.Date == date.Date)
                        return true;
                }
            }

            return false;
        }
    }
}