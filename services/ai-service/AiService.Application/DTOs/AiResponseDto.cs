namespace AiService.Application.DTOs;

public class AiResponseDto
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public string? Data { get; set; }
}
