using Application.Models;
using Application.Validators;

namespace UnitaryTests.Validator
{
    public class UserCostumerModelValidatorTests
    {
        [Fact]
        public void Validate()
        {
            // Arrange
            var userCostumerModel = new UserCostumerModel
            {
            };

            var result = new UserCostumerModelValidator(userCostumerModel);

            // Act
            var errorMessages = result.GetErrorMessages();
            int count = errorMessages.Count();

            // Assert
            Assert.True(count > 0);
        }
    }
}
