using Domain.Extensions;

namespace UnitaryTests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void CleanInputShouldReturnEmptyString()
        {
            // Arrange
            var input = "'''";

            // Act
            var result = input.CleanInput();

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void CleanInputShouldReturnCleanedString()
        {
            // Arrange
            var input = "Test User'*/-/-*()%$#!#,'";

            // Act
            var result = input.CleanInput();

            // Assert
            Assert.Equal("Test User", result);
        }     
    }
}
