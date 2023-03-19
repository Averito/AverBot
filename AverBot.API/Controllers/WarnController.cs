using AverBot.API.DTO;
using AverBot.API.Hubs;
using AverBot.API.Models;
using AverBot.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AverBot.API.Controllers;

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
    public async Task<ActionResult<List<Warn>>> GetWarnsByGuildUserIdAndServerId(int guildUserId, int serverId) =>
        Ok(await _warnService.GetWarnsByGuildUserIdAndServerId(guildUserId, serverId));
}