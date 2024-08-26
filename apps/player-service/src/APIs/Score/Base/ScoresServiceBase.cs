using Microsoft.EntityFrameworkCore;
using PlayerService.APIs;
using PlayerService.APIs.Common;
using PlayerService.APIs.Dtos;
using PlayerService.APIs.Errors;
using PlayerService.APIs.Extensions;
using PlayerService.Infrastructure;
using PlayerService.Infrastructure.Models;

namespace PlayerService.APIs;

public abstract class ScoresServiceBase : IScoresService
{
    protected readonly PlayerServiceDbContext _context;

    public ScoresServiceBase(PlayerServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Score
    /// </summary>
    public async Task<Score> CreateScore(ScoreCreateInput createDto)
    {
        var score = new ScoreDbModel
        {
            CreatedAt = createDto.CreatedAt,
            ScoreValue = createDto.ScoreValue,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            score.Id = createDto.Id;
        }
        if (createDto.Game != null)
        {
            score.Game = await _context
                .Games.Where(game => createDto.Game.Id == game.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Player != null)
        {
            score.Player = await _context
                .Players.Where(player => createDto.Player.Id == player.Id)
                .FirstOrDefaultAsync();
        }

        _context.Scores.Add(score);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ScoreDbModel>(score.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Score
    /// </summary>
    public async Task DeleteScore(ScoreWhereUniqueInput uniqueId)
    {
        var score = await _context.Scores.FindAsync(uniqueId.Id);
        if (score == null)
        {
            throw new NotFoundException();
        }

        _context.Scores.Remove(score);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Scores
    /// </summary>
    public async Task<List<Score>> Scores(ScoreFindManyArgs findManyArgs)
    {
        var scores = await _context
            .Scores.Include(x => x.Player)
            .Include(x => x.Game)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return scores.ConvertAll(score => score.ToDto());
    }

    /// <summary>
    /// Meta data about Score records
    /// </summary>
    public async Task<MetadataDto> ScoresMeta(ScoreFindManyArgs findManyArgs)
    {
        var count = await _context.Scores.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Score
    /// </summary>
    public async Task<Score> Score(ScoreWhereUniqueInput uniqueId)
    {
        var scores = await this.Scores(
            new ScoreFindManyArgs { Where = new ScoreWhereInput { Id = uniqueId.Id } }
        );
        var score = scores.FirstOrDefault();
        if (score == null)
        {
            throw new NotFoundException();
        }

        return score;
    }

    /// <summary>
    /// Update one Score
    /// </summary>
    public async Task UpdateScore(ScoreWhereUniqueInput uniqueId, ScoreUpdateInput updateDto)
    {
        var score = updateDto.ToModel(uniqueId);

        if (updateDto.Game != null)
        {
            score.Game = await _context
                .Games.Where(game => updateDto.Game.Id == game.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Player != null)
        {
            score.Player = await _context
                .Players.Where(player => updateDto.Player.Id == player.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(score).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Scores.Any(e => e.Id == score.Id))
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
    /// Get a Game record for Score
    /// </summary>
    public async Task<Game> GetGame(ScoreWhereUniqueInput uniqueId)
    {
        var score = await _context
            .Scores.Where(score => score.Id == uniqueId.Id)
            .Include(score => score.Game)
            .FirstOrDefaultAsync();
        if (score == null)
        {
            throw new NotFoundException();
        }
        return score.Game.ToDto();
    }

    /// <summary>
    /// Get a Player record for Score
    /// </summary>
    public async Task<Player> GetPlayer(ScoreWhereUniqueInput uniqueId)
    {
        var score = await _context
            .Scores.Where(score => score.Id == uniqueId.Id)
            .Include(score => score.Player)
            .FirstOrDefaultAsync();
        if (score == null)
        {
            throw new NotFoundException();
        }
        return score.Player.ToDto();
    }
}
