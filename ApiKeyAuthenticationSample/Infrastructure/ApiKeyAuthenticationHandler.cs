using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using static ApiKeyAuthenticationSample.Constants;

namespace ApiKeyAuthenticationSample.Infrastructure;

public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
{
    private readonly IApiKeyHashingService _apiKeyHashingService;
    private readonly IApiKeyStore _apiKeyStore;

    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<ApiKeyAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IApiKeyHashingService apiKeyHashingService,
        IApiKeyStore apiKeyStore)
        : base(options, logger, encoder, clock)
    {
        _apiKeyHashingService = apiKeyHashingService;
        _apiKeyStore = apiKeyStore;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(ApiKeyAuthentication.ApiKeyHeaderName, out var values))
        {
            return AuthenticateResult.Fail("No API key present in request");
        }

        var apiKey = values.ToString();

        if (string.IsNullOrEmpty(apiKey))
        {
            return AuthenticateResult.Fail("No API key present in request");
        }

        var apiKeyHash = await _apiKeyHashingService.HashApiKey(apiKey);

        if (string.IsNullOrEmpty(apiKeyHash))
        {
            return AuthenticateResult.Fail("Invalid API key.");
        }

        var isValid = await _apiKeyStore.ContainsApiKey(apiKeyHash);

        if (!isValid)
        {
            return AuthenticateResult.Fail("Invalid API key.");
        }

        var identity = new ClaimsIdentity(ApiKeyAuthentication.AuthenticationType);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, ApiKeyAuthentication.SchemeName);

        return AuthenticateResult.Success(ticket);
    }
}