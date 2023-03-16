using AverBot.API.Main.DTO;
using AverBot.API.Main.Models;
using AverBot.API.Main.Services;
using Microsoft.AspNetCore.Mvc;

namespace AverBot.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : MainController<UserController>
{
    private readonly UserService _userService;
    
    public UserController(ILogger<UserController> logger, UserService userService) : base(logger)
    {
        _userService = userService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<List<User>>> GetAll() => Ok(await _userService.GetUsers());

    [HttpPost("Create")]
    public async Task<ActionResult<User>> Create(CreateUserDTO createUserDto) =>
        CreatedAtAction(nameof(Create), await _userService.CreateUser(createUserDto));
}