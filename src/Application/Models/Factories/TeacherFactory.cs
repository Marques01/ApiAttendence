using Application.Models.Request;
using Domain.Entities;
using Domain.Extensions;

namespace Application.Models.Factories
{
    public static class TeacherFactory
    {
        public static Teacher CreateTeacher(TeacherRequestModel requestModel)
        {
            return new Teacher
            {
                Name = requestModel.Name.CapitalizeFirstLetters().Trim(),
                Registration = requestModel.Registration.ToUpper().Trim(),
                Email = requestModel.Email.ToLower().Trim(),
                Enabled = requestModel.Enabled,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
                DisabledAt = requestModel.Enabled ? DateTime.MinValue : DateTime.Now
            };
        }
    }
}