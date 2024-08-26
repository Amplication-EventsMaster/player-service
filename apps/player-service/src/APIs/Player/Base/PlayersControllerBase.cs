using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayerService.APIs;
using PlayerService.APIs.Common;
using PlayerService.APIs.Dtos;
using PlayerService.APIs.Errors;

namespace PlayerService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PlayersControllerBase : ControllerBase
{
    protected readonly IPlayersService _service;

    public PlayersControllerBase(IPlayersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Player
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Player>> CreatePlayer(PlayerCreateInput input)
    {
        var player = await _service.CreatePlayer(input);

        return CreatedAtAction(nameof(Player), new { id = player.Id }, player);
    }

    /// <summary>
    /// Delete one Player
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeletePlayer([FromRoute()] PlayerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeletePlayer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Players
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Player>>> Players([FromQuery()] PlayerFindManyArgs filter)
    {
        return Ok(await _service.Players(filter));
    }

    /// <summary>
    /// Meta data about Player records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PlayersMeta(
        [FromQuery()] PlayerFindManyArgs filter
    )
    {
        return Ok(await _service.PlayersMeta(filter));
    }

    /// <summary>
    /// Get one Player
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Player>> Player([FromRoute()] PlayerWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Player(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Player
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdatePlayer(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
        [FromQuery()] PlayerUpdateInput playerUpdateDto
    )
    {
        try
        {
            await _service.UpdatePlayer(uniqueId, playerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Scores records to Player
    /// </summary>
    [HttpPost("{Id}/scores")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectScores(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
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
    /// Disconnect multiple Scores records from Player
    /// </summary>
    [HttpDelete("{Id}/scores")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectScores(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
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
    /// Find multiple Scores records for Player
    /// </summary>
    [HttpGet("{Id}/scores")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Score>>> FindScores(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
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
    /// Update multiple Scores records for Player
    /// </summary>
    [HttpPatch("{Id}/scores")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateScores(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
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
