using PlayerService.Infrastructure;

namespace PlayerService.APIs;

public class GamesService : GamesServiceBase
{
    public GamesService(PlayerServiceDbContext context)
        : base(context) { }
}
