using Application.Models.Request;
using Domain.Validation;

namespace Application.Validators
{
    public class AttendanceRequestModelValidator : CostumerModelValidator<AttendanceRequestModel>
    {
        public AttendanceRequestModelValidator(AttendanceRequestModel model) : base(model)
        {
            ValidateFor(a => a.ScheduleId)
                .MustBeGreaterThanZero("ScheduleId deve ser maior que zero");

            ValidateFor(a => a.StudentId)
                .MustBeGreaterThanZero("StudentId deve ser maior que zero");

            ValidateFor(a => a.Status)
                .NotEmpty("Status não pode ser vazio");
        }
    }
}