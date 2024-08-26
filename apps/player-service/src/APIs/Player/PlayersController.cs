using Microsoft.AspNetCore.Mvc;

namespace PlayerService.APIs;

[ApiController()]
public class PlayersController : PlayersControllerBase
{
    public PlayersController(IPlayersService service)
        : base(service) { }
}
