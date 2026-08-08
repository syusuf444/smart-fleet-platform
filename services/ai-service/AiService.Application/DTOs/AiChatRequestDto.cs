namespace AiService.Application.DTOs;

public class AiChatRequestDto
{
    public string Message { get; set; } = string.Empty;

    public string? Context { get; set; }
}
