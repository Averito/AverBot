using System.Security.Claims;
using AverBot.API.DTO;
using AverBot.API.Models;
using AverBot.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AverBot.API.Controllers;

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
    
    [HttpGet("{id}")]
    public async Task<ActionResult<List<User>>> GetById(int id) => 
        Ok(await _userService.GetUserById(id));


    [HttpPatch("Edit")]
    public async Task<ActionResult<User>> Edit(EditUserDTO editUserDto)
    {
        var currentUser = await _userService.Me(HttpContext);
        return CreatedAtAction(nameof(Edit), await _userService.EditUser(currentUser, editUserDto));
    }
}