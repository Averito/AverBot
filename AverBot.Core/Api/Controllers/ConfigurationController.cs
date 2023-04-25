using AverBot.Core.Domain.Entities;
using AverBot.Core.Infrastructure.Constants;
using AverBot.Core.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AverBot.Core.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ConfigurationController : MainController<ConfigurationController>
{
    private readonly ConfigurationService _configurationService;
    
    public ConfigurationController(ILogger<ConfigurationController> logger, ConfigurationService configurationService) : base(logger)
    {
        _configurationService = configurationService;
    }

    [HttpPost("Create/{serverId}")]
    public async Task<ActionResult<Configuration>> Create(int serverId) =>
        CreatedAtAction(nameof(Create), await _configurationService.Create(serverId));
}