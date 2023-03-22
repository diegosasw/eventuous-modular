using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("health")]
[AllowAnonymous]
public class HealthController
    : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public HealthController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }
        
    /// <summary>
    /// Gets service's health
    /// </summary>
    [HttpGet]
    public IActionResult Get()
    {
        var environmentName = _environment.EnvironmentName;
        var currentUtcDateTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        var assemblyName = typeof(Program).Assembly.GetName();
        var result = $"Assembly={assemblyName}, Environment={environmentName}, CurrentUtcTime={currentUtcDateTime}";
        return Ok(result);
    }
}
