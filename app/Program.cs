var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

var tokenConfigured = !string.IsNullOrWhiteSpace(
    Environment.GetEnvironmentVariable("APP_TOKEN"));

app.Logger.LogInformation(
    "Starting application. APP_MESSAGE configured: {MessageConfigured}, APP_TOKEN configured: {TokenConfigured}",
    !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("APP_MESSAGE")),
    tokenConfigured);

app.MapControllers();

app.Urls.Add("http://0.0.0.0:8080");

app.Run();
