using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerService.Infrastructure.Models;

[Table("Players")]
public class PlayerDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [StringLength(1000)]
    public string? Phone { get; set; }

    public List<ScoreDbModel>? Scores { get; set; } = new List<ScoreDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
