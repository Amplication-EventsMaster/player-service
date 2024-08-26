using Microsoft.AspNetCore.Mvc;
using PlayerService.APIs.Common;
using PlayerService.Infrastructure.Models;

namespace PlayerService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class PlayerFindManyArgs : FindManyInput<Player, PlayerWhereInput> { }
