namespace ApiKeyAuthenticationSample.Infrastructure;

public interface IApiKeyHashingService
{
    Task<string> HashApiKey(string apiKey);
}