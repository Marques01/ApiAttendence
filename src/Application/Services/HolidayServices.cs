using Application.Models.Factories;
using Application.Models.Request;
using Application.Models.Response;
using Application.Services.Interfaces;
using Application.Validators;
using Domain.CostumerExceptions;
using Domain.Entities;
using Domain.Repository.Interfaces;
using System.Net;

namespace Application.Services
{
    public class HolidayServices : IHolidayServices
    {
        private readonly IUnitOfWork _uof;

        public HolidayServices(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<HolidayResponseModel> CreateAsync(HolidayRequestModel requestModel)
        {
            try
            {
                ValidateModel(requestModel);

                Holiday holidayModel = HolidayFactory.CreateHoliday(requestModel);

                await _uof.HolidayRepository.CreateAsync(holidayModel);
                await _uof.CommitAsync();

                return new HolidayResponseModel()
                {
                    IsSuccess = true,
                    Message = "Feriado cadastrado com sucesso",
                    StatusCode = HttpStatusCode.Created,
                    Model = holidayModel
                };
            }
            catch (ArgumentException arg)
            {
                throw new ArgumentException(arg.Message);
            }
            catch (CustomerValidationException val)
            {
                throw new CustomerValidationException(val.ErrorMessages);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ValidateModel(HolidayRequestModel model)
        {
            var validator = new HolidayRequestModelValidator(model);

            var errorMessages = validator.GetErrorMessages();

            if (errorMessages.Count() > 0)
                throw new CustomerValidationException(errorMessages);
        }
    }
}