using AverBot.Core.DTO;
using AverBot.Core.Domain.Entities;
using AverBot.Core.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AverBot.Core.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class GuildUserController : MainController<GuildUserController>
{
    private readonly GuildUserService _guildUserService;
    
    public GuildUserController(ILogger<GuildUserController> logger, GuildUserService guildUserService) : base(logger)
    {
        _guildUserService = guildUserService;
    }

    [HttpPost("Create/{serverId}")]
    public async Task<ActionResult<GuildUser>> Create(CreateGuildUserDTO createGuildUserDto, int serverId) =>
        CreatedAtAction(nameof(Create), await _guildUserService.Create(createGuildUserDto, serverId));
    
    [HttpPost("AddToServer")]
    public async Task<ActionResult<GuildUser>> AddToServer(AddToServerDTO addToServerDto) =>
        CreatedAtAction(nameof(AddToServer), await _guildUserService.AddToServer(addToServerDto));
}