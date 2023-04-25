using AverBot.Core.Infrastructure.Constants;
using AverBot.Core.DTO;
using AverBot.Core.Domain.Entities;
using AverBot.Core.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AverBot.Core.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ServerController : MainController<ServerController>
{
    private readonly ServerService _serverService;
    
    public ServerController(ILogger<ServerController> logger, ServerService serverService) : base(logger)
    {
        _serverService = serverService;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Server>> Create(CreateServerDTO createServerDto)
    {
        var userService = HttpContext.RequestServices.GetService<UserService>();
        if (userService == null) throw new BadHttpRequestException(ExceptionMessage.SomethingWentWrong);
        
        var userId = userService.GetUserIdFromHttpContext(HttpContext);
        return CreatedAtAction(nameof(Create), await _serverService.Create(createServerDto, userId));
    }
}