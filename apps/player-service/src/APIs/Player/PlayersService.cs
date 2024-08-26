using PlayerService.Infrastructure;

namespace PlayerService.APIs;

public class PlayersService : PlayersServiceBase
{
    public PlayersService(PlayerServiceDbContext context)
        : base(context) { }
}
