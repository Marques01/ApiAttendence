using Application.Models.Request;
using Domain.Validation;

namespace Application.Validators
{
    public class HabilitationCostumerModelValidator : CostumerModelValidator<HabilitationRequestModel>
    {
        public HabilitationCostumerModelValidator(HabilitationRequestModel model) : base(model)
        {
            ValidateFor(h => h.Name)
                .NotEmpty("Nome da habilidade não pode ser vazio")
                .MinLength(3, "Nome da habilidade deve ter no mínimo 3 caracteres")
                .MaxLength(100, "Nome da habilidade deve ter no máximo 100 caracteres");

            ValidateFor(h => h.Description)
                .NotEmpty("Descrição não pode ser vazia")
                .MinLength(5, "Descrição deve ter no mínimo 5 caracteres")
                .MaxLength(500, "Descrição deve ter no máximo 500 caracteres");
        }
    }
}