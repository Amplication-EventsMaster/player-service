using PlayerService.Infrastructure;

namespace PlayerService.APIs;

public class ScoresService : ScoresServiceBase
{
    public ScoresService(PlayerServiceDbContext context)
        : base(context) { }
}
