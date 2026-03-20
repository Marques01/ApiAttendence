using Application.Models.Request;
using Domain.Validation;

namespace Application.Validators
{
    public class TeacherHabilitationCostumerModelValidator : CostumerModelValidator<TeacherHabilitationRequestModel>
    {
        public TeacherHabilitationCostumerModelValidator(TeacherHabilitationRequestModel model) : base(model)
        {
            ValidateFor(t => t.TeacherId)
                .NotNegative("TeacherId não pode ser negativo");

            ValidateFor(t => t.HabilitationId)
                .NotNegative("HabilitationId não pode ser negativo");
        }
    }
}