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
    public class TeacherHabilitationServices : ITeacherHabilitationServices
    {
        private readonly IUnitOfWork _uof;

        public TeacherHabilitationServices(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<TeacherHabilitationResponseModel> CreateAsync(TeacherHabilitationRequestModel requestModel)
        {
            try
            {
                ValidateModel(requestModel);

                var teacher = await GetTeacherAsync(requestModel.TeacherId);
                if (teacher.TeacherId == 0)
                    throw new ArgumentException("Professor não encontrado");

                var habilitation = await GetHabilitationAsync(requestModel.HabilitationId);
                if (habilitation.HabilitationId == 0)
                    throw new ArgumentException("Habilidade não encontrada");

                var teacherHabilitationExists = await GetTeacherHabilitationAsync(requestModel.TeacherId, requestModel.HabilitationId);
                if (teacherHabilitationExists.TeacherHabilitationId > 0)
                    throw new ArgumentException("Este professor já possui essa habilidade");

                TeacherHabilitation teacherHabilitationModel = TeacherHabilitationFactory.CreateTeacherHabilitation(requestModel);

                await _uof.TeacherHabilitationRepository.CreateAsync(teacherHabilitationModel);
                await _uof.CommitAsync();

                return new TeacherHabilitationResponseModel()
                {
                    IsSuccess = true,
                    Message = "Habilidade vinculada ao professor com sucesso",
                    StatusCode = HttpStatusCode.Created,
                    Model = teacherHabilitationModel
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

        public async Task<TeacherHabilitationResponseModel> DeleteAsync(int teacherHabilitationId)
        {
            try
            {
                var teacherHabilitation = await GetTeacherHabilitationByIdAsync(teacherHabilitationId);

                if (teacherHabilitation.TeacherHabilitationId == 0)
                    throw new ArgumentException("Vínculo de habilidade não encontrado");

                await _uof.TeacherHabilitationRepository.DeleteAsync(teacherHabilitation);
                await _uof.CommitAsync();

                return new TeacherHabilitationResponseModel()
                {
                    IsSuccess = true,
                    Message = "Habilidade removida do professor com sucesso",
                    StatusCode = HttpStatusCode.OK
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

        public async Task<List<TeacherHabilitationResponseModel>> GetTeacherHabilitationsAsync(int teacherId)
        {
            try
            {
                var teacher = await GetTeacherAsync(teacherId);
                if (teacher.TeacherId == 0)
                    throw new ArgumentException("Professor não encontrado");

                var teacherHabilitations = await _uof.TeacherHabilitationRepository.GetTeacherHabilitationsAsync(false, teacherId);

                return teacherHabilitations
                    .Select(th => new TeacherHabilitationResponseModel()
                    {
                        IsSuccess = true,
                        StatusCode = HttpStatusCode.OK,
                        Model = th
                    })
                    .ToList();
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

        private async Task<Teacher> GetTeacherAsync(int teacherId)
        {
            return await _uof.TeacherRepository.GetTeacherByRegistrationAsync(false, teacherId.ToString());
        }

        private async Task<Habilitation> GetHabilitationAsync(int habilitationId)
        {
            return await _uof.HabilitationRepository.GetHabilitationByIdAsync(false, habilitationId);
        }

        private async Task<TeacherHabilitation> GetTeacherHabilitationAsync(int teacherId, int habilitationId)
        {
            return await _uof.TeacherHabilitationRepository.GetByTeacherAndHabilitationAsync(false, teacherId, habilitationId);
        }

        private async Task<TeacherHabilitation> GetTeacherHabilitationByIdAsync(int teacherHabilitationId)
        {
            return await _uof.TeacherHabilitationRepository.GetByIdAsync(false, teacherHabilitationId);
        }

        private void ValidateModel(TeacherHabilitationRequestModel model)
        {
            var validator = new TeacherHabilitationCostumerModelValidator(model);

            var errorMessages = validator.GetErrorMessages();

            if (errorMessages.Count() > 0)
                throw new CustomerValidationException(errorMessages);
        }
    }
}