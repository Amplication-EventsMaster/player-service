namespace PlayerService.APIs.Dtos;

public class ScoreUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Game { get; set; }

    public string? Id { get; set; }

    public string? Player { get; set; }

    public int? ScoreValue { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
