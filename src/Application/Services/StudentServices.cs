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
    public class StudentServices : IStudentServices
    {
        private readonly IUnitOfWork _uof;

        public StudentServices(IUnitOfWork uof)
        {
            _uof = uof;
        }
        public async Task<StudentResponseModel> CreateAsync(StudentRequestModel requestModel)
        {
            try
            {
                ValidateModel(requestModel);

                var student = await GetStudentByRegistrationAsync(requestModel.Registration);

                if (student.StudentId > 0)
                    throw new ArgumentException("Ooops... Já existe um estudante cadastrado com essa matrícula");

                Student studentModel = StudentFactory.CreateStudent(requestModel);

                await _uof.StudentRepository.CreateAsync(studentModel);
                await _uof.CommitAsync();

                return new StudentResponseModel()
                {
                    IsSuccess = true,
                    Message = "Student created successfully",
                    StatusCode = HttpStatusCode.Created,
                    Model = studentModel
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

        private async Task<Student> GetStudentByRegistrationAsync(string registration)
        {
            return await _uof.StudentRepository.GetStudentByRegistrationAsync(false, registration);
        }

        private void ValidateModel(StudentRequestModel model)
        {
            var studentRequestModelValidator = new StudentCostumerModelValidator(model);

            var errorMessages = studentRequestModelValidator.GetErrorMessages();

            if (errorMessages.Count() > 0)
                throw new CustomerValidationException(errorMessages);
        }
    }
}
