using System.Security.Cryptography;

namespace ApiKeyAuthenticationSample.Infrastructure;

public class Sha256ApiKeyHashingService : IApiKeyHashingService
{
    public Task<string> HashApiKey(string apiKey)
    {
        try
        {
            var keyBytes = Convert.FromBase64String(apiKey);
            var keyHashBytes = SHA256.HashData(keyBytes);

            return Task.FromResult(Convert.ToBase64String(keyHashBytes));
        }
        catch (FormatException)
        {
            return Task.FromResult(string.Empty);
        }
    }
}