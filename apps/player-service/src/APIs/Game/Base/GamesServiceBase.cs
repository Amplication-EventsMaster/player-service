using Microsoft.EntityFrameworkCore;
using PlayerService.APIs;
using PlayerService.APIs.Common;
using PlayerService.APIs.Dtos;
using PlayerService.APIs.Errors;
using PlayerService.APIs.Extensions;
using PlayerService.Infrastructure;
using PlayerService.Infrastructure.Models;

namespace PlayerService.APIs;

public abstract class GamesServiceBase : IGamesService
{
    protected readonly PlayerServiceDbContext _context;

    public GamesServiceBase(PlayerServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Game
    /// </summary>
    public async Task<Game> CreateGame(GameCreateInput createDto)
    {
        var game = new GameDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Genre = createDto.Genre,
            Title = createDto.Title,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            game.Id = createDto.Id;
        }
        if (createDto.Scores != null)
        {
            game.Scores = await _context
                .Scores.Where(score => createDto.Scores.Select(t => t.Id).Contains(score.Id))
                .ToListAsync();
        }

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<GameDbModel>(game.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Game
    /// </summary>
    public async Task DeleteGame(GameWhereUniqueInput uniqueId)
    {
        var game = await _context.Games.FindAsync(uniqueId.Id);
        if (game == null)
        {
            throw new NotFoundException();
        }

        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Games
    /// </summary>
    public async Task<List<Game>> Games(GameFindManyArgs findManyArgs)
    {
        var games = await _context
            .Games.Include(x => x.Scores)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return games.ConvertAll(game => game.ToDto());
    }

    /// <summary>
    /// Meta data about Game records
    /// </summary>
    public async Task<MetadataDto> GamesMeta(GameFindManyArgs findManyArgs)
    {
        var count = await _context.Games.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Game
    /// </summary>
    public async Task<Game> Game(GameWhereUniqueInput uniqueId)
    {
        var games = await this.Games(
            new GameFindManyArgs { Where = new GameWhereInput { Id = uniqueId.Id } }
        );
        var game = games.FirstOrDefault();
        if (game == null)
        {
            throw new NotFoundException();
        }

        return game;
    }

    /// <summary>
    /// Update one Game
    /// </summary>
    public async Task UpdateGame(GameWhereUniqueInput uniqueId, GameUpdateInput updateDto)
    {
        var game = updateDto.ToModel(uniqueId);

        if (updateDto.Scores != null)
        {
            game.Scores = await _context
                .Scores.Where(score => updateDto.Scores.Select(t => t.Id).Contains(score.Id))
                .ToListAsync();
        }

        _context.Entry(game).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Games.Any(e => e.Id == game.Id))
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
    /// Connect multiple Scores records to Game
    /// </summary>
    public async Task ConnectScores(
        GameWhereUniqueInput uniqueId,
        ScoreWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Games.Include(x => x.Scores)
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
    /// Disconnect multiple Scores records from Game
    /// </summary>
    public async Task DisconnectScores(
        GameWhereUniqueInput uniqueId,
        ScoreWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Games.Include(x => x.Scores)
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
    /// Find multiple Scores records for Game
    /// </summary>
    public async Task<List<Score>> FindScores(
        GameWhereUniqueInput uniqueId,
        ScoreFindManyArgs gameFindManyArgs
    )
    {
        var scores = await _context
            .Scores.Where(m => m.GameId == uniqueId.Id)
            .ApplyWhere(gameFindManyArgs.Where)
            .ApplySkip(gameFindManyArgs.Skip)
            .ApplyTake(gameFindManyArgs.Take)
            .ApplyOrderBy(gameFindManyArgs.SortBy)
            .ToListAsync();

        return scores.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Scores records for Game
    /// </summary>
    public async Task UpdateScores(
        GameWhereUniqueInput uniqueId,
        ScoreWhereUniqueInput[] childrenIds
    )
    {
        var game = await _context
            .Games.Include(t => t.Scores)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (game == null)
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

        game.Scores = children;
        await _context.SaveChangesAsync();
    }
}
