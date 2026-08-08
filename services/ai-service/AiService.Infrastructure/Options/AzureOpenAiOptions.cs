namespace AiService.Infrastructure.Options;

public class AzureOpenAiOptions
{
    public const string SectionName = "AzureOpenAI";

    public string Endpoint { get; set; } = string.Empty;

    public string ApiKey { get; set; } = string.Empty;

    public string DeploymentName { get; set; } = string.Empty;

    public string ApiVersion { get; set; } = "2024-10-21";
}
