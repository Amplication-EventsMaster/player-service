using Microsoft.EntityFrameworkCore;
using PlayerService.APIs;
using PlayerService.APIs.Common;
using PlayerService.APIs.Dtos;
using PlayerService.APIs.Errors;
using PlayerService.APIs.Extensions;
using PlayerService.Infrastructure;
using PlayerService.Infrastructure.Models;

namespace PlayerService.APIs;

public abstract class PlayersServiceBase : IPlayersService
{
    protected readonly PlayerServiceDbContext _context;

    public PlayersServiceBase(PlayerServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Player
    /// </summary>
    public async Task<Player> CreatePlayer(PlayerCreateInput createDto)
    {
        var player = new PlayerDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            Name = createDto.Name,
            Phone = createDto.Phone,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            player.Id = createDto.Id;
        }
        if (createDto.Scores != null)
        {
            player.Scores = await _context
                .Scores.Where(score => createDto.Scores.Select(t => t.Id).Contains(score.Id))
                .ToListAsync();
        }

        _context.Players.Add(player);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PlayerDbModel>(player.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Player
    /// </summary>
    public async Task DeletePlayer(PlayerWhereUniqueInput uniqueId)
    {
        var player = await _context.Players.FindAsync(uniqueId.Id);
        if (player == null)
        {
            throw new NotFoundException();
        }

        _context.Players.Remove(player);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Players
    /// </summary>
    public async Task<List<Player>> Players(PlayerFindManyArgs findManyArgs)
    {
        var players = await _context
            .Players.Include(x => x.Scores)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return players.ConvertAll(player => player.ToDto());
    }

    /// <summary>
    /// Meta data about Player records
    /// </summary>
    public async Task<MetadataDto> PlayersMeta(PlayerFindManyArgs findManyArgs)
    {
        var count = await _context.Players.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Player
    /// </summary>
    public async Task<Player> Player(PlayerWhereUniqueInput uniqueId)
    {
        var players = await this.Players(
            new PlayerFindManyArgs { Where = new PlayerWhereInput { Id = uniqueId.Id } }
        );
        var player = players.FirstOrDefault();
        if (player == null)
        {
            throw new NotFoundException();
        }

        return player;
    }

    /// <summary>
    /// Update one Player
    /// </summary>
    public async Task UpdatePlayer(PlayerWhereUniqueInput uniqueId, PlayerUpdateInput updateDto)
    {
        var player = updateDto.ToModel(uniqueId);

        if (updateDto.Scores != null)
        {
            player.Scores = await _context
                .Scores.Where(score => updateDto.Scores.Select(t => t.Id).Contains(score.Id))
                .ToListAsync();
        }

        _context.Entry(player).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Players.Any(e => e.Id == player.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Connect multiple Scores records to Player
    /// </summary>
    public async Task ConnectScores(
        PlayerWhereUniqueInput uniqueId,
        ScoreWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Players.Include(x => x.Scores)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Scores.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Scores);

        foreach (var child in childrenToConnect)
        {
            parent.Scores.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Scores records from Player
    /// </summary>
    public async Task DisconnectScores(
        PlayerWhereUniqueInput uniqueId,
        ScoreWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Players.Include(x => x.Scores)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Scores.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Scores?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Scores records for Player
    /// </summary>
    public async Task<List<Score>> FindScores(
        PlayerWhereUniqueInput uniqueId,
        ScoreFindManyArgs playerFindManyArgs
    )
    {
        var scores = await _context
            .Scores.Where(m => m.PlayerId == uniqueId.Id)
            .ApplyWhere(playerFindManyArgs.Where)
            .ApplySkip(playerFindManyArgs.Skip)
            .ApplyTake(playerFindManyArgs.Take)
            .ApplyOrderBy(playerFindManyArgs.SortBy)
            .ToListAsync();

        return scores.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Scores records for Player
    /// </summary>
    public async Task UpdateScores(
        PlayerWhereUniqueInput uniqueId,
        ScoreWhereUniqueInput[] childrenIds
    )
    {
        var player = await _context
            .Players.Include(t => t.Scores)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (player == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Scores.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        player.Scores = children;
        await _context.SaveChangesAsync();
    }
}
