using Application.Models.Request;
using Domain.Entities;
using Domain.Extensions;

namespace Application.Models.Factories
{
    public static class HabilitationFactory
    {
        public static Habilitation CreateHabilitation(HabilitationRequestModel requestModel)
        {
            return new Habilitation
            {
                Name = requestModel.Name.CapitalizeFirstLetters().Trim(),
                Description = requestModel.Description.Trim(),
                Enabled = requestModel.Enabled,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
                DisabledAt = requestModel.Enabled ? DateTime.MinValue : DateTime.Now
            };
        }
    }
}