using Application.Models.Request;
using Domain.Entities;
using Domain.Extensions;

namespace Application.Models.Factories
{
    public static class ClassesFactory
    {
        public static Classes CreateClasses(ClassesRequestModel requestModel)
        {
            return new Classes
            {
                Name = requestModel.Name.CapitalizeFirstLetters().Trim(),
                TeacherId = requestModel.TeacherId,
                ClassroomId = requestModel.ClassroomId,
                StartDate = requestModel.StartDate,
                EndDate = requestModel.EndDate,
                Notes = requestModel.Notes.Trim(),
                Enabled = requestModel.Enabled,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue
            };
        }
    }
}