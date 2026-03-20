using Application.Models;
using Domain.Validation;

namespace Application.Validators
{
    public class UserCostumerModelValidator : CostumerModelValidator<UserCostumerModel>
    {
        public UserCostumerModelValidator(UserCostumerModel model) : base(model)
        {
            ValidateFor(u => u.Login)
                .NotEmpty("Login não pode ser vazio")
                .MinLength(3, "Login deve ter no mínimo 3 caracteres")
                .MaxLength(100, "Login deve ter no máximo 100 caracteres");

            ValidateFor(u => u.Password)
                .NotEmpty("Senha não pode ser vazia")
                .MinLength(6, "Senha deve ter no mínimo 6 caracteres")
                .MaxLength(32, "Senha deve ter no máximo 32 caracteres")
                .IsStrongPassword("Senha deve conter letra maiúscula, minúscula, número e caractere especial");
            
            ValidateFor(u => u.Name)
                .NotEmpty("Nome não pode ser vazio")
                .MinLength(3, "Nome deve ter no mínimo 3 caracteres")
                .MaxLength(100, "Nome deve ter no máximo 100 caracteres");
        }
    }
}
