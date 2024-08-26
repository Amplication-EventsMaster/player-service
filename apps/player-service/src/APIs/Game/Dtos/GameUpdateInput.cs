namespace PlayerService.APIs.Dtos;

public class GameUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Genre { get; set; }

    public string? Id { get; set; }

    public List<string>? Scores { get; set; }

    public string? Title { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
