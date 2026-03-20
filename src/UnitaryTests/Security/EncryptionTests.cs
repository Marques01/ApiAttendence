using Domain.Security.Interfaces;
using Infrastructure.Security;

namespace UnitaryTests.Security
{
    public class EncryptionTests
    {
        private readonly IEncryption _encryption;

        public EncryptionTests()
        {
            _encryption = new Encryption();
        }

        [Fact]
        public void EncryptShouldReturnEncryptedString()
        {
            // Arrange
            string password = "123";

            string salt = _encryption.GenerateSalt();

            // Act
            string result = _encryption.GenerateHash(password, salt);

            // Assert
            Assert.NotEqual(password, result);
        }

        [Fact]
        public void VerifyHashShouldReturnTrue()
        {
            // Arrange
            string password = "123";

            string salt = _encryption.GenerateSalt();

            string hash = _encryption.GenerateHash(password, salt);

            // Act
            bool result = _encryption.VerifyHash(password, salt, hash);

            // Assert
            Assert.True(result);
        }
    }
}
