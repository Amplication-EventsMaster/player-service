using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayerService.APIs;
using PlayerService.APIs.Common;
using PlayerService.APIs.Dtos;
using PlayerService.APIs.Errors;

namespace PlayerService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ScoresControllerBase : ControllerBase
{
    protected readonly IScoresService _service;

    public ScoresControllerBase(IScoresService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Score
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Score>> CreateScore(ScoreCreateInput input)
    {
        var score = await _service.CreateScore(input);

        return CreatedAtAction(nameof(Score), new { id = score.Id }, score);
    }

    /// <summary>
    /// Delete one Score
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteScore([FromRoute()] ScoreWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteScore(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Scores
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Score>>> Scores([FromQuery()] ScoreFindManyArgs filter)
    {
        return Ok(await _service.Scores(filter));
    }

    /// <summary>
    /// Meta data about Score records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ScoresMeta([FromQuery()] ScoreFindManyArgs filter)
    {
        return Ok(await _service.ScoresMeta(filter));
    }

    /// <summary>
    /// Get one Score
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Score>> Score([FromRoute()] ScoreWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Score(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Score
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateScore(
        [FromRoute()] ScoreWhereUniqueInput uniqueId,
        [FromQuery()] ScoreUpdateInput scoreUpdateDto
    )
    {
        try
        {
            await _service.UpdateScore(uniqueId, scoreUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Game record for Score
    /// </summary>
    [HttpGet("{Id}/game")]
    public async Task<ActionResult<List<Game>>> GetGame(
        [FromRoute()] ScoreWhereUniqueInput uniqueId
    )
    {
        var game = await _service.GetGame(uniqueId);
        return Ok(game);
    }

    /// <summary>
    /// Get a Player record for Score
    /// </summary>
    [HttpGet("{Id}/player")]
    public async Task<ActionResult<List<Player>>> GetPlayer(
        [FromRoute()] ScoreWhereUniqueInput uniqueId
    )
    {
        var player = await _service.GetPlayer(uniqueId);
        return Ok(player);
    }
}
