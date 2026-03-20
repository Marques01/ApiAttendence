using Application.Models.Request;
using Domain.Entities;

namespace Application.Models.Factories
{
    public static class RfidCardFactory
    {
        public static RfidCard CreateRfidCard(RfidCardRequestModel requestModel)
        {
            return new RfidCard
            {
                Code = requestModel.Code.ToUpper().Trim(),
                Number = requestModel.Number,
                Enabled = requestModel.Enabled,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.MinValue,
                DisabledAt = requestModel.Enabled ? DateTime.MinValue : DateTime.Now
            };
        }
    }
}