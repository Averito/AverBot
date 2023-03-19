using AverBot.API.DTO;
using AverBot.API.Models;
using AverBot.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AverBot.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class GuildUserController : MainController<GuildUserController>
{
    public readonly GuildUserService _guildUserService;
    
    public GuildUserController(ILogger<GuildUserController> logger, GuildUserService guildUserService) : base(logger)
    {
        _guildUserService = guildUserService;
    }

    [HttpPost("Create/{serverId}")]
    public async Task<ActionResult<GuildUser>> Create(CreateGuildUserDTO createGuildUserDto, int serverId) =>
        CreatedAtAction(nameof(Create), await _guildUserService.Create(createGuildUserDto, serverId));
}