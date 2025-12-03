using ApiKeyAuthenticationSample;

Console.Title = "API Key Authentication Sample";

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

await app.RunAsync();