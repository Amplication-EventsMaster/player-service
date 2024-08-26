using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayerService.APIs;
using PlayerService.APIs.Common;
using PlayerService.APIs.Dtos;
using PlayerService.APIs.Errors;

namespace PlayerService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class GamesControllerBase : ControllerBase
{
    protected readonly IGamesService _service;

    public GamesControllerBase(IGamesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Game
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Game>> CreateGame(GameCreateInput input)
    {
        var game = await _service.CreateGame(input);

        return CreatedAtAction(nameof(Game), new { id = game.Id }, game);
    }

    /// <summary>
    /// Delete one Game
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteGame([FromRoute()] GameWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteGame(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Games
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Game>>> Games([FromQuery()] GameFindManyArgs filter)
    {
        return Ok(await _service.Games(filter));
    }

    /// <summary>
    /// Meta data about Game records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> GamesMeta([FromQuery()] GameFindManyArgs filter)
    {
        return Ok(await _service.GamesMeta(filter));
    }

    /// <summary>
    /// Get one Game
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Game>> Game([FromRoute()] GameWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Game(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Game
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateGame(
        [FromRoute()] GameWhereUniqueInput uniqueId,
        [FromQuery()] GameUpdateInput gameUpdateDto
    )
    {
        try
        {
            await _service.UpdateGame(uniqueId, gameUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Scores records to Game
    /// </summary>
    [HttpPost("{Id}/scores")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectScores(
        [FromRoute()] GameWhereUniqueInput uniqueId,
        [FromQuery()] ScoreWhereUniqueInput[] scoresId
    )
    {
        try
        {
            await _service.ConnectScores(uniqueId, scoresId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Scores records from Game
    /// </summary>
    [HttpDelete("{Id}/scores")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectScores(
        [FromRoute()] GameWhereUniqueInput uniqueId,
        [FromBody()] ScoreWhereUniqueInput[] scoresId
    )
    {
        try
        {
            await _service.DisconnectScores(uniqueId, scoresId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Scores records for Game
    /// </summary>
    [HttpGet("{Id}/scores")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Score>>> FindScores(
        [FromRoute()] GameWhereUniqueInput uniqueId,
        [FromQuery()] ScoreFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindScores(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Scores records for Game
    /// </summary>
    [HttpPatch("{Id}/scores")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateScores(
        [FromRoute()] GameWhereUniqueInput uniqueId,
        [FromBody()] ScoreWhereUniqueInput[] scoresId
    )
    {
        try
        {
            await _service.UpdateScores(uniqueId, scoresId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
