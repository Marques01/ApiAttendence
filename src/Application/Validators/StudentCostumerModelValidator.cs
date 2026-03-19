using Application.Models.Request;
using Domain.Validation;

namespace Application.Validators
{
    public class StudentCostumerModelValidator : CostumerModelValidator<StudentRequestModel>
    {
        public StudentCostumerModelValidator(StudentRequestModel model) : base(model)
        {
            ValidateFor(x => x.RfidCardId)
                .NotNegative("O Id do cartão RFID não pode ser negativo.");

            ValidateFor(x => x.Registration)
                .NotEmpty("A matrícula do aluno é obrigatória.")
                .MinLength(5, "A matrícula do aluno deve conter pelo menos 5 caracteres.")
                .MaxLength(20, "A matrícula do aluno deve conter no máximo 20 caracteres.");

            ValidateFor(x => x.Name)
                .NotEmpty("O nome do aluno é obrigatório.")
                .MinLength(2, "O nome do aluno deve conter pelo menos 2 caracteres.")
                .MaxLength(100, "O nome do aluno deve conter no máximo 100 caracteres.");
        }
    }
}
