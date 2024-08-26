using Microsoft.AspNetCore.Mvc;

namespace PlayerService.APIs;

[ApiController()]
public class ScoresController : ScoresControllerBase
{
    public ScoresController(IScoresService service)
        : base(service) { }
}
