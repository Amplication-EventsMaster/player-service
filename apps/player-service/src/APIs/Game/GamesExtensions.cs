using PlayerService.APIs.Dtos;
using PlayerService.Infrastructure.Models;

namespace PlayerService.APIs.Extensions;

public static class GamesExtensions
{
    public static Game ToDto(this GameDbModel model)
    {
        return new Game
        {
            CreatedAt = model.CreatedAt,
            Genre = model.Genre,
            Id = model.Id,
            Scores = model.Scores?.Select(x => x.Id).ToList(),
            Title = model.Title,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static GameDbModel ToModel(this GameUpdateInput updateDto, GameWhereUniqueInput uniqueId)
    {
        var game = new GameDbModel
        {
            Id = uniqueId.Id,
            Genre = updateDto.Genre,
            Title = updateDto.Title
        };

        if (updateDto.CreatedAt != null)
        {
            game.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            game.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return game;
    }
}
