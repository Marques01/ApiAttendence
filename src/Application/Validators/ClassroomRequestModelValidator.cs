using Application.Models.Request;
using Domain.Validation;

namespace Application.Validators
{
    public class ClassroomRequestModelValidator : CostumerModelValidator<ClassroomRequestModel>
    {
        public ClassroomRequestModelValidator(ClassroomRequestModel model) : base(model)
        {
            ValidateFor(c => c.Name)
                .NotEmpty("Nome da sala não pode ser vazio")
                .MinLength(3, "Nome da sala deve ter no mínimo 3 caracteres")
                .MaxLength(100, "Nome da sala deve ter no máximo 100 caracteres");

            ValidateFor(c => c.Capacity)
                .MustBeGreaterThanZero("Capacidade deve ser maior que zero");

            ValidateFor(c => c.Location)
                .NotEmpty("Localização não pode ser vazia")
                .MinLength(3, "Localização deve ter no mínimo 3 caracteres")
                .MaxLength(100, "Localização deve ter no máximo 100 caracteres");
        }
    }
}