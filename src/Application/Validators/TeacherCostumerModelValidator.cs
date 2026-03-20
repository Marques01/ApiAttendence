using Application.Models.Request;
using Domain.Validation;

namespace Application.Validators
{
    public class TeacherCostumerModelValidator : CostumerModelValidator<TeacherRequestModel>
    {
        public TeacherCostumerModelValidator(TeacherRequestModel model) : base(model)
        {
            ValidateFor(t => t.Name)
                .NotEmpty("Nome não pode ser vazio")
                .MinLength(3, "Nome deve ter no mínimo 3 caracteres")
                .MaxLength(100, "Nome deve ter no máximo 100 caracteres");

            ValidateFor(t => t.Registration)
                .NotEmpty("Matrícula não pode ser vazia")
                .MinLength(3, "Matrícula deve ter no mínimo 3 caracteres")
                .MaxLength(20, "Matrícula deve ter no máximo 20 caracteres");

            ValidateFor(t => t.Email)
                .NotEmpty("Email não pode ser vazio")
                .IsValidEmail("Email inválido");
        }
    }
}