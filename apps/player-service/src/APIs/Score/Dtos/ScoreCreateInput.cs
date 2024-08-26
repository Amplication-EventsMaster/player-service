namespace PlayerService.APIs.Dtos;

public class ScoreCreateInput
{
    public DateTime CreatedAt { get; set; }

    public Game? Game { get; set; }

    public string? Id { get; set; }

    public Player? Player { get; set; }

    public int? ScoreValue { get; set; }

    public DateTime UpdatedAt { get; set; }
}
