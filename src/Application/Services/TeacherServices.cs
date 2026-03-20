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
    public class TeacherServices : ITeacherServices
    {
        private readonly IUnitOfWork _uof;

        public TeacherServices(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<TeacherResponseModel> CreateAsync(TeacherRequestModel requestModel)
        {
            try
            {
                ValidateModel(requestModel);

                var teacherByRegistration = await GetTeacherByRegistrationAsync(requestModel.Registration);

                if (teacherByRegistration.TeacherId > 0)
                    throw new ArgumentException("Ooops... Já existe um professor cadastrado com essa matrícula");

                var teacherByEmail = await GetTeacherByEmailAsync(requestModel.Email);

                if (teacherByEmail.TeacherId > 0)
                    throw new ArgumentException("Ooops... Já existe um professor cadastrado com esse email");

                Teacher teacherModel = TeacherFactory.CreateTeacher(requestModel);

                await _uof.TeacherRepository.CreateAsync(teacherModel);
                await _uof.CommitAsync();

                return new TeacherResponseModel()
                {
                    IsSuccess = true,
                    Message = "Teacher created successfully",
                    StatusCode = HttpStatusCode.Created,
                    Model = teacherModel
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

        private async Task<Teacher> GetTeacherByRegistrationAsync(string registration)
        {
            return await _uof.TeacherRepository.GetTeacherByRegistrationAsync(false, registration);
        }

        private async Task<Teacher> GetTeacherByEmailAsync(string email)
        {
            return await _uof.TeacherRepository.GetTeacherByEmailAsync(false, email);
        }

        private void ValidateModel(TeacherRequestModel model)
        {
            var teacherRequestModelValidator = new TeacherCostumerModelValidator(model);

            var errorMessages = teacherRequestModelValidator.GetErrorMessages();

            if (errorMessages.Count() > 0)
                throw new CustomerValidationException(errorMessages);
        }
    }
}