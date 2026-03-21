using Application.Models.Request;
using Domain.Validation;

namespace Application.Validators
{
    public class HolidayRequestModelValidator : CostumerModelValidator<HolidayRequestModel>
    {
        public HolidayRequestModelValidator(HolidayRequestModel model) : base(model)
        {
            ValidateFor(h => h.Name)
                .NotEmpty("Nome do feriado não pode ser vazio")
                .MinLength(3, "Nome do feriado deve ter no mínimo 3 caracteres")
                .MaxLength(100, "Nome do feriado deve ter no máximo 100 caracteres");
        }
    }
}