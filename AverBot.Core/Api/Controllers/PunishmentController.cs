using AverBot.Core.Domain.Entities;
using AverBot.Core.DTO;
using AverBot.Core.Infrastructure.Constants;
using AverBot.Core.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AverBot.Core.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PunishmentController : MainController<PunishmentController>
{
    private readonly ConfigurationPunishmentService _configurationPunishmentService;
    
    public PunishmentController(ILogger<PunishmentController> logger, ConfigurationPunishmentService configurationPunishmentService) : base(logger)
    {
        _configurationPunishmentService = configurationPunishmentService;
    }
    
    [HttpPost("Create")]
    public async Task<ActionResult<ConfigurationPunishment>> Create(CreatePunishmentDTO createPunishmentDto)
    {
        var serverService = HttpContext.RequestServices.GetService<ServerService>();
        if (serverService == null) throw new BadHttpRequestException(ExceptionMessage.SomethingWentWrong);
        
        var serverId = serverService.GetCurrentServerIdFromHttpContext(HttpContext);
        var currentServer = await serverService.GetById(serverId);
        var configurationId = currentServer.Configuration.Id;

        return await _configurationPunishmentService.Create(createPunishmentDto, configurationId);
    }
}