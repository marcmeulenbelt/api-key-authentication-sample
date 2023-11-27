namespace ApiKeyAuthenticationSample.Infrastructure;

public interface IApiKeyStore
{
    Task<bool> ContainsApiKey(string apiKey);
}