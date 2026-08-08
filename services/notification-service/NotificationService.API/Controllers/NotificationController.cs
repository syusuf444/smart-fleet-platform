using Microsoft.AspNetCore.Mvc;

namespace NotificationService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    [HttpGet("health")]
    public IActionResult Health() => Ok(new { status = "Notification Service Ready" });
}
