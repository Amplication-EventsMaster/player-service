using PlayerService.APIs.Common;
using PlayerService.APIs.Dtos;

namespace PlayerService.APIs;

public interface IScoresService
{
    /// <summary>
    /// Create one Score
    /// </summary>
    public Task<Score> CreateScore(ScoreCreateInput score);

    /// <summary>
    /// Delete one Score
    /// </summary>
    public Task DeleteScore(ScoreWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Scores
    /// </summary>
    public Task<List<Score>> Scores(ScoreFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Score records
    /// </summary>
    public Task<MetadataDto> ScoresMeta(ScoreFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Score
    /// </summary>
    public Task<Score> Score(ScoreWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Score
    /// </summary>
    public Task UpdateScore(ScoreWhereUniqueInput uniqueId, ScoreUpdateInput updateDto);

    /// <summary>
    /// Get a Game record for Score
    /// </summary>
    public Task<Game> GetGame(ScoreWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Player record for Score
    /// </summary>
    public Task<Player> GetPlayer(ScoreWhereUniqueInput uniqueId);
}
