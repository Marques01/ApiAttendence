using Application.Models.Request;
using Domain.Entities;

namespace Application.Models.Factories
{
    public static class TeacherHabilitationFactory
    {
        public static TeacherHabilitation CreateTeacherHabilitation(TeacherHabilitationRequestModel requestModel)
        {
            return new TeacherHabilitation
            {
                TeacherId = requestModel.TeacherId,
                HabilitationId = requestModel.HabilitationId,
                Enabled = requestModel.Enabled,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
                DisabledAt = requestModel.Enabled ? DateTime.MinValue : DateTime.Now
            };
        }
    }
}