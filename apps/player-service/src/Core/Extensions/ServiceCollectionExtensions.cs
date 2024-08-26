using PlayerService.APIs;

namespace PlayerService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IGamesService, GamesService>();
        services.AddScoped<IPlayersService, PlayersService>();
        services.AddScoped<IScoresService, ScoresService>();
    }
}
