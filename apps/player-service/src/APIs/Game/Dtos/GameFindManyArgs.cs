using Microsoft.AspNetCore.Mvc;
using PlayerService.APIs.Common;
using PlayerService.Infrastructure.Models;

namespace PlayerService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class GameFindManyArgs : FindManyInput<Game, GameWhereInput> { }
