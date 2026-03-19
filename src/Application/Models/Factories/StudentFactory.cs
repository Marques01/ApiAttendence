using Domain.Entities;
using Domain.Extensions;
using Application.Models.Request;

namespace Application.Factories
{
    public static class StudentFactory
    {
        public static Student CreateStudent(StudentRequestModel requestModel)
        {
            return new Student
            {
                Name = requestModel.Name.CapitalizeFirstLetters().Trim(),
                Registration = requestModel.Registration.ToUpper().Trim(),
                Enabled = requestModel.Enabled,
                RfidCardId = requestModel.RfidCardId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
                DisabledAt = requestModel.Enabled ? DateTime.MinValue : DateTime.Now
            };
        }
    }
}