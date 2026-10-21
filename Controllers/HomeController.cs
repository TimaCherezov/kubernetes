using Microsoft.AspNetCore.Mvc;

namespace KuberHomeWork.Controllers;

[ApiController]
[Route("/")]
public class HomeController(ILogger<HomeController> logger) : ControllerBase
{
    // Идентификатор автора приложения.
    private const string AuthorName = "tim";

    [HttpGet]
    public IActionResult Get()
    {
        var appMessage = Environment.GetEnvironmentVariable("APP_MESSAGE");

        var tokenConfigured = !string.IsNullOrWhiteSpace(
            Environment.GetEnvironmentVariable("APP_TOKEN"));

        logger.LogInformation(
            "GET / requested. APP_MESSAGE present: {MessagePresent}, APP_TOKEN configured: {TokenConfigured}",
            !string.IsNullOrWhiteSpace(appMessage),
            tokenConfigured);

        return Ok(new
        {
            message = appMessage,
            author = AuthorName,
            tokenConfigured
        });
    }
}
