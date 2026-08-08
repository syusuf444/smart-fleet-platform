using AiService.Application.DTOs;
using AiService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AiService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AiAssistantController : ControllerBase
{
    private readonly IAiAssistantService _assistantService;

    public AiAssistantController(IAiAssistantService assistantService)
    {
        _assistantService = assistantService;
    }

    [HttpPost("chat")]
    public async Task<IActionResult> Chat(
        [FromBody] AiChatRequestDto request,
        CancellationToken cancellationToken)
    {
        var response =
            await _assistantService.AskAsync(request, cancellationToken);

        return Ok(response);
    }

    [HttpPost("fleet-health")]
    public async Task<IActionResult> AnalyzeFleetHealth(
        [FromBody] FleetHealthAnalysisRequestDto request,
        CancellationToken cancellationToken)
    {
        var response =
            await _assistantService.AnalyzeFleetHealthAsync(
                request,
                cancellationToken);

        return Ok(response);
    }
}
