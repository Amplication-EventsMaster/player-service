using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerService.Infrastructure.Models;

[Table("Scores")]
public class ScoreDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? GameId { get; set; }

    [ForeignKey(nameof(GameId))]
    public GameDbModel? Game { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? PlayerId { get; set; }

    [ForeignKey(nameof(PlayerId))]
    public PlayerDbModel? Player { get; set; } = null;

    [Range(-999999999, 999999999)]
    public int? ScoreValue { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
