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
    public class HabilitationServices : IHabilitationServices
    {
        private readonly IUnitOfWork _uof;

        public HabilitationServices(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<HabilitationResponseModel> CreateAsync(HabilitationRequestModel requestModel)
        {
            try
            {
                ValidateModel(requestModel);

                var habilitationExists = await GetHabilitationByNameAsync(requestModel.Name);

                if (habilitationExists.HabilitationId > 0)
                    throw new ArgumentException("Ooops... Já existe uma habilidade cadastrada com esse nome");

                Habilitation habilitationModel = HabilitationFactory.CreateHabilitation(requestModel);

                await _uof.HabilitationRepository.CreateAsync(habilitationModel);
                await _uof.CommitAsync();

                return new HabilitationResponseModel()
                {
                    IsSuccess = true,
                    Message = "Habilitation created successfully",
                    StatusCode = HttpStatusCode.Created,
                    Model = habilitationModel
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

        private async Task<Habilitation> GetHabilitationByNameAsync(string name)
        {
            return await _uof.HabilitationRepository.GetHabilitationByNameAsync(false, name);
        }

        private void ValidateModel(HabilitationRequestModel model)
        {
            var habilitationRequestModelValidator = new HabilitationCostumerModelValidator(model);

            var errorMessages = habilitationRequestModelValidator.GetErrorMessages();

            if (errorMessages.Count() > 0)
                throw new CustomerValidationException(errorMessages);
        }
    }
}