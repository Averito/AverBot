using AverBot.Core.Domain.Entities;
using AverBot.Core.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AverBot.Core.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WarnController : MainController<WarnController>
{
    private readonly WarnService _warnService;

    public WarnController(ILogger<WarnController> logger, WarnService warnService) : base(logger)
    {
        _warnService = warnService;
    }

    [HttpGet("GetWarns")]
    public async Task<ActionResult<List<Warn>>> GetWarnsByGuildUserIdAndServerId(ulong guildUserDiscordId, int serverId) =>
        Ok(await _warnService.GetWarnsByGuildUserDiscordIdAndServerId(guildUserDiscordId, serverId));
}