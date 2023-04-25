using AverBot.Core.DTO;
using AverBot.Core.Domain.Entities;
using AverBot.Core.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AverBot.Core.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : MainController<UserController>
{
    private readonly UserService _userService;
    
    public UserController(ILogger<UserController> logger, UserService userService) : base(logger)
    {
        _userService = userService;
    }

    [HttpPatch("Edit")]
    public async Task<ActionResult<User>> Edit(EditUserDTO editUserDto)
    {
        var currentUser = await _userService.Me(HttpContext);
        return CreatedAtAction(nameof(Edit), await _userService.EditUser(currentUser, editUserDto));
    }
}