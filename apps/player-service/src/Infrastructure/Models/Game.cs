using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerService.Infrastructure.Models;

[Table("Games")]
public class GameDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Genre { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public List<ScoreDbModel>? Scores { get; set; } = new List<ScoreDbModel>();

    [StringLength(1000)]
    public string? Title { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
