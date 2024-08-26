using PlayerService.APIs.Dtos;
using PlayerService.Infrastructure.Models;

namespace PlayerService.APIs.Extensions;

public static class PlayersExtensions
{
    public static Player ToDto(this PlayerDbModel model)
    {
        return new Player
        {
            CreatedAt = model.CreatedAt,
            Email = model.Email,
            Id = model.Id,
            Name = model.Name,
            Phone = model.Phone,
            Scores = model.Scores?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PlayerDbModel ToModel(
        this PlayerUpdateInput updateDto,
        PlayerWhereUniqueInput uniqueId
    )
    {
        var player = new PlayerDbModel
        {
            Id = uniqueId.Id,
            Email = updateDto.Email,
            Name = updateDto.Name,
            Phone = updateDto.Phone
        };

        if (updateDto.CreatedAt != null)
        {
            player.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            player.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return player;
    }
}
