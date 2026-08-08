using AiService.Application.DTOs;

namespace AiService.Application.Interfaces;

public interface IAiAssistantService
{
    Task<AiResponseDto> AskAsync(
        AiChatRequestDto request,
        CancellationToken cancellationToken);

    Task<AiResponseDto> AnalyzeFleetHealthAsync(
        FleetHealthAnalysisRequestDto request,
        CancellationToken cancellationToken);
}
