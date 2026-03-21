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
    public class ClassesServices : IClassesServices
    {
        private readonly IUnitOfWork _uof;

        public ClassesServices(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<ClassesResponseModel> CreateAsync(ClassesRequestModel requestModel)
        {
            try
            {
                ValidateModel(requestModel);

                var teacher = await GetTeacherAsync(requestModel.TeacherId);
                if (teacher.TeacherId == 0)
                    throw new ArgumentException("Professor não encontrado");

                var classroom = await GetClassroomAsync(requestModel.ClassroomId);
                if (classroom.Id == 0)
                    throw new ArgumentException("Sala de aula não encontrada");

                if (requestModel.StartDate >= requestModel.EndDate)
                    throw new ArgumentException("Data de início deve ser anterior à data de término");

                Classes classesModel = ClassesFactory.CreateClasses(requestModel);

                await _uof.ClassesRepository.CreateAsync(classesModel);
                await _uof.CommitAsync();

                return new ClassesResponseModel()
                {
                    IsSuccess = true,
                    Message = "Aula cadastrada com sucesso",
                    StatusCode = HttpStatusCode.Created,
                    Model = classesModel
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

        private async Task<Teacher> GetTeacherAsync(int teacherId)
        {
            return await _uof.TeacherRepository.GetTeacherByIdAsync(false, teacherId);
        }

        private async Task<Classroom> GetClassroomAsync(int classroomId)
        {
            return await _uof.ClassroomRepository.GetClassroomByIdAsync(false, classroomId);
        }

        private void ValidateModel(ClassesRequestModel model)
        {
            var validator = new ClassesRequestModelValidator(model);

            var errorMessages = validator.GetErrorMessages();

            if (errorMessages.Count() > 0)
                throw new CustomerValidationException(errorMessages);
        }
    }
}