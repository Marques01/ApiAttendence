using Application.Models.Request;
using Domain.Entities;
using Domain.Extensions;

namespace Application.Models.Factories
{
    public static class ClassroomFactory
    {
        public static Classroom CreateClassroom(ClassroomRequestModel requestModel)
        {
            return new Classroom
            {
                Name = requestModel.Name.CapitalizeFirstLetters().Trim(),
                Capacity = requestModel.Capacity,
                Location = requestModel.Location.Trim(),
                IsAvailable = requestModel.IsAvailable,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue
            };
        }
    }
}