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
    public class RfidCardServices : IRfidCardServices
    {
        private readonly IUnitOfWork _uof;

        public RfidCardServices(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<RfidCardResponseModel> CreateAsync(RfidCardRequestModel requestModel)
        {
            try
            {
                ValidateModel(requestModel);

                RfidCard rfidCardModel = RfidCardFactory.CreateRfidCard(requestModel);

                await _uof.RfidCardRepository.CreateAsync(rfidCardModel);
                await _uof.CommitAsync();

                return new RfidCardResponseModel()
                {
                    IsSuccess = true,
                    Message = "RFID Card created successfully",
                    StatusCode = HttpStatusCode.Created,
                    Model = rfidCardModel
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

        private void ValidateModel(RfidCardRequestModel model)
        {
            var rfidCardRequestModelValidator = new RfidCardCostumerModelValidator(model);

            var errorMessages = rfidCardRequestModelValidator.GetErrorMessages();

            if (errorMessages.Count() > 0)
                throw new CustomerValidationException(errorMessages);
        }
    }
}