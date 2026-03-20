using Application.Models.Request;
using Domain.Validation;

namespace Application.Validators
{
    public class RfidCardCostumerModelValidator : CostumerModelValidator<RfidCardRequestModel>
    {
        public RfidCardCostumerModelValidator(RfidCardRequestModel model) : base(model)
        {
            ValidateFor(x => x.Code)
                .NotEmpty("O código do cartão RFID é obrigatório.")
                .MinLength(3, "O código do cartão RFID deve conter pelo menos 3 caracteres.")
                .MaxLength(50, "O código do cartão RFID deve conter no máximo 50 caracteres.");

            ValidateFor(x => x.Number)
                .NotNegative("O número do cartão RFID não pode ser negativo.");
        }
    }
}