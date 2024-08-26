using PlayerService.APIs.Dtos;
using PlayerService.Infrastructure.Models;

namespace PlayerService.APIs.Extensions;

public static class ScoresExtensions
{
    public static Score ToDto(this ScoreDbModel model)
    {
        return new Score
        {
            CreatedAt = model.CreatedAt,
            Game = model.GameId,
            Id = model.Id,
            Player = model.PlayerId,
            ScoreValue = model.ScoreValue,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ScoreDbModel ToModel(
        this ScoreUpdateInput updateDto,
        ScoreWhereUniqueInput uniqueId
    )
    {
        var score = new ScoreDbModel { Id = uniqueId.Id, ScoreValue = updateDto.ScoreValue };

        if (updateDto.CreatedAt != null)
        {
            score.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Game != null)
        {
            score.GameId = updateDto.Game;
        }
        if (updateDto.Player != null)
        {
            score.PlayerId = updateDto.Player;
        }
        if (updateDto.UpdatedAt != null)
        {
            score.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return score;
    }
}
