using PlayerService.APIs.Common;
using PlayerService.APIs.Dtos;

namespace PlayerService.APIs;

public interface IPlayersService
{
    /// <summary>
    /// Create one Player
    /// </summary>
    public Task<Player> CreatePlayer(PlayerCreateInput player);

    /// <summary>
    /// Delete one Player
    /// </summary>
    public Task DeletePlayer(PlayerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Players
    /// </summary>
    public Task<List<Player>> Players(PlayerFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Player records
    /// </summary>
    public Task<MetadataDto> PlayersMeta(PlayerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Player
    /// </summary>
    public Task<Player> Player(PlayerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Player
    /// </summary>
    public Task UpdatePlayer(PlayerWhereUniqueInput uniqueId, PlayerUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Scores records to Player
    /// </summary>
    public Task ConnectScores(PlayerWhereUniqueInput uniqueId, ScoreWhereUniqueInput[] scoresId);

    /// <summary>
    /// Disconnect multiple Scores records from Player
    /// </summary>
    public Task DisconnectScores(PlayerWhereUniqueInput uniqueId, ScoreWhereUniqueInput[] scoresId);

    /// <summary>
    /// Find multiple Scores records for Player
    /// </summary>
    public Task<List<Score>> FindScores(
        PlayerWhereUniqueInput uniqueId,
        ScoreFindManyArgs ScoreFindManyArgs
    );

    /// <summary>
    /// Update multiple Scores records for Player
    /// </summary>
    public Task UpdateScores(PlayerWhereUniqueInput uniqueId, ScoreWhereUniqueInput[] scoresId);
}
