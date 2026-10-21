var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

// Признак того, что учебный токен настроен.
// Само значение APP_TOKEN не логируется и не возвращается наружу.
var tokenConfigured = !string.IsNullOrWhiteSpace(
    Environment.GetEnvironmentVariable("APP_TOKEN"));

app.Logger.LogInformation(
    "Starting application. APP_MESSAGE configured: {MessageConfigured}, APP_TOKEN configured: {TokenConfigured}",
    !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("APP_MESSAGE")),
    tokenConfigured);

app.MapControllers();

// Слушаем на всех интерфейсах (0.0.0.0), чтобы приложению можно было
// обратиться извне контейнера по опубликованному порту.
app.Urls.Add("http://0.0.0.0:8080");

app.Run();
