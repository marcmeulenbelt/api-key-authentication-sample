namespace ApiKeyAuthenticationSample.Infrastructure;

public class InMemoryApiKeyStore : IApiKeyStore
{
    private static readonly string[] KeyStore =
    {
        // key: xBjv3RyKFINDGZ+YECadYsMiIHbrYJ9V7ptm55SXDbLWa3FexvkxcL91jlXrK70e
        "I+irZzAXz4h8Wh7cM9Rt3gI1THhHoC1Fqgl2Elnx3/g=",

        // key: HC/IL03rjETn+4kC05tqS9jR7qVS4jpz2R0ryAlDRrfjLqLnecYp/PMITuzhzuU2
        "Uu+oyDISzUt+xs160hObCPTp+Jud3WorrsdArTBjLAI=",

        // key: 2r3/uwXUnKtiBWHjtvZGBu7xp7PJP+h7SDpNQEO91Zo9CBUMl/M0g2QDjm3N0ku9
        "zmNetF9nydJTPZ8z7K/GFcgNGL1Q30hBCD2N2R2NsbY="
    };

    public Task<bool> ContainsApiKey(string apiKey) => Task.FromResult(KeyStore.Contains(apiKey));
}