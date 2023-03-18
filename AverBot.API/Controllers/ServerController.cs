using AverBot.API.Constants;
using AverBot.API.DTO;
using AverBot.API.Models;
using AverBot.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AverBot.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ServerController : MainController<ServerController>
{
    public readonly ServerService _serverService;
    
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