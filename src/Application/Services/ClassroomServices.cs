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
    public class ClassroomServices : IClassroomServices
    {
        private readonly IUnitOfWork _uof;

        public ClassroomServices(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<ClassroomResponseModel> CreateAsync(ClassroomRequestModel requestModel)
        {
            try
            {
                ValidateModel(requestModel);

                var classroomExists = await GetClassroomByNameAsync(requestModel.Name);
                if (classroomExists.Id > 0)
                    throw new ArgumentException("Já existe uma sala com esse nome cadastrada");

                Classroom classroomModel = ClassroomFactory.CreateClassroom(requestModel);

                await _uof.ClassroomRepository.CreateAsync(classroomModel);
                await _uof.CommitAsync();

                return new ClassroomResponseModel()
                {
                    IsSuccess = true,
                    Message = "Sala de aula cadastrada com sucesso",
                    StatusCode = HttpStatusCode.Created,
                    Model = classroomModel
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

        private async Task<Classroom> GetClassroomByNameAsync(string name)
        {
            return await _uof.ClassroomRepository.GetClassroomByNameAsync(false, name);
        }

        private void ValidateModel(ClassroomRequestModel model)
        {
            var validator = new ClassroomRequestModelValidator(model);

            var errorMessages = validator.GetErrorMessages();

            if (errorMessages.Count() > 0)
                throw new CustomerValidationException(errorMessages);
        }
    }
}