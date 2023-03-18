using AverBot.API.Models;
using AverBot.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AverBot.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : MainController<UserController>
{
    private readonly UserService _userService;
    
    public UserController(ILogger<UserController> logger, UserService userService) : base(logger)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<List<User>>> GetById(int id)
    {
        return Ok(await _userService.GetUserById(id));
    }

    [HttpPatch("Edit")]
    public async Task<ActionResult<User>> Edit(User user)
    {
        return CreatedAtAction(nameof(Edit), await _userService.EditUser(user));
    }
}