using Application.Models.Request;
using Domain.Validation;

namespace Application.Validators
{
    public class ClassesRequestModelValidator : CostumerModelValidator<ClassesRequestModel>
    {
        public ClassesRequestModelValidator(ClassesRequestModel model) : base(model)
        {
            ValidateFor(c => c.Name)
                .NotEmpty("Nome da aula não pode ser vazio")
                .MinLength(3, "Nome da aula deve ter no mínimo 3 caracteres")
                .MaxLength(100, "Nome da aula deve ter no máximo 100 caracteres");

            ValidateFor(c => c.TeacherId)
                .MustBeGreaterThanZero("TeacherId deve ser maior que zero");

            ValidateFor(c => c.ClassroomId)
                .MustBeGreaterThanZero("ClassroomId deve ser maior que zero");
        }
    }
}