namespace PlayerService.APIs.Dtos;

public class PlayerCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public List<Score>? Scores { get; set; }

    public DateTime UpdatedAt { get; set; }
}
