using AverBot.Core.DTO;
using AverBot.Core.Domain.Entities;
using AverBot.Core.Infrastructure.Constants;
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

    [HttpPost("Create")]
    public async Task<ActionResult<GuildUser>> Create(CreateGuildUserDTO createGuildUserDto)
    {
        var serverService = HttpContext.RequestServices.GetService<ServerService>();
        if (serverService == null) throw new BadHttpRequestException(ExceptionMessage.SomethingWentWrong);
        
        var serverId = serverService.GetCurrentServerIdFromHttpContext(HttpContext);
        return CreatedAtAction(nameof(Create), await _guildUserService.Create(createGuildUserDto, serverId));
    }

    [HttpPost("AddToServer")]
    public async Task<ActionResult<GuildUser>> AddToServer(AddToServerDTO addToServerDto)
    {
        var serverService = HttpContext.RequestServices.GetService<ServerService>();
        if (serverService == null) throw new BadHttpRequestException(ExceptionMessage.SomethingWentWrong);
        
        var serverId = serverService.GetCurrentServerIdFromHttpContext(HttpContext);
        return CreatedAtAction(nameof(AddToServer), await _guildUserService.AddToServer(addToServerDto, serverId));
    }
}