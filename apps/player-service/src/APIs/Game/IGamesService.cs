using PlayerService.APIs.Common;
using PlayerService.APIs.Dtos;

namespace PlayerService.APIs;

public interface IGamesService
{
    /// <summary>
    /// Create one Game
    /// </summary>
    public Task<Game> CreateGame(GameCreateInput game);

    /// <summary>
    /// Delete one Game
    /// </summary>
    public Task DeleteGame(GameWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Games
    /// </summary>
    public Task<List<Game>> Games(GameFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Game records
    /// </summary>
    public Task<MetadataDto> GamesMeta(GameFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Game
    /// </summary>
    public Task<Game> Game(GameWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Game
    /// </summary>
    public Task UpdateGame(GameWhereUniqueInput uniqueId, GameUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Scores records to Game
    /// </summary>
    public Task ConnectScores(GameWhereUniqueInput uniqueId, ScoreWhereUniqueInput[] scoresId);

    /// <summary>
    /// Disconnect multiple Scores records from Game
    /// </summary>
    public Task DisconnectScores(GameWhereUniqueInput uniqueId, ScoreWhereUniqueInput[] scoresId);

    /// <summary>
    /// Find multiple Scores records for Game
    /// </summary>
    public Task<List<Score>> FindScores(
        GameWhereUniqueInput uniqueId,
        ScoreFindManyArgs ScoreFindManyArgs
    );

    /// <summary>
    /// Update multiple Scores records for Game
    /// </summary>
    public Task UpdateScores(GameWhereUniqueInput uniqueId, ScoreWhereUniqueInput[] scoresId);
}
