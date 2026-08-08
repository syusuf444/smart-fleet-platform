using MediatR;

namespace AIService.Application.Features.Assistant.Queries;

public class GetNaturalLanguageInsightQuery : IRequest<string>
{
    public string UserPrompt { get; set; } = string.Empty;
    public string ContextType { get; set; } = "FleetSummary"; // FleetSummary, Maintenance, etc.
}