using Microsoft.AspNetCore.Mvc;

namespace PlayerService.APIs;

[ApiController()]
public class GamesController : GamesControllerBase
{
    public GamesController(IGamesService service)
        : base(service) { }
}
