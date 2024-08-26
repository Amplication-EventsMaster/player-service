namespace PlayerService.APIs.Dtos;

public class PlayerUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public List<string>? Scores { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
