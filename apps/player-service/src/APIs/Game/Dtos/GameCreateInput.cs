namespace PlayerService.APIs.Dtos;

public class GameCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Genre { get; set; }

    public string? Id { get; set; }

    public List<Score>? Scores { get; set; }

    public string? Title { get; set; }

    public DateTime UpdatedAt { get; set; }
}
