using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlayerService.Infrastructure.Models;

namespace PlayerService.Infrastructure;

public class PlayerServiceDbContext : IdentityDbContext<IdentityUser>
{
    public PlayerServiceDbContext(DbContextOptions<PlayerServiceDbContext> options)
        : base(options) { }

    public DbSet<PlayerDbModel> Players { get; set; }

    public DbSet<ScoreDbModel> Scores { get; set; }

    public DbSet<GameDbModel> Games { get; set; }
}
