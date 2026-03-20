using Domain.Entities;

namespace Application.Models.Factories
{
    public class UserCostumerFactory
    {
        public static User CreateFromUserCostumerModel(UserCostumerModel userCostumerModel)
        {
            User user = new()
            {
                Login = userCostumerModel.Login.Trim().ToLower(),
                Enabled = true,
                FailedCount = 0,
                CreateAt = DateTime.Now,
                LastLogin = DateTime.MinValue,
                UpdateAt = DateTime.MinValue,
                Name = userCostumerModel.Name.Trim()
            };

            return user;
        }
    }
}
