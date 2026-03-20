namespace Domain.Security.Interfaces
{
    public interface IEncryption
    {
        bool VerifyHash(string plainText, string salt, string hash);

        string GenerateHash(string plainText, string salt);

        string GenerateSalt(int size = 32);
    }
}
