using AverBot.Core.DTO;
using AverBot.Core.Infrastructure.Constants;
using AverBot.Core.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AverBot.Core.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : MainController<AuthController>
{
    private readonly AuthService _authService;
    
    public AuthController(ILogger<AuthController> logger, AuthService authService) : base(logger)
    {
        _authService = authService;
    }

    [HttpPost("Registration")]
    public async Task<ActionResult<RegistrationResponseDTO>> Registration(RegistrationDTO registrationDto) =>
        CreatedAtAction(nameof(Registration), await _authService.Registration(registrationDto));


    [HttpPost("Login")]
    public async Task<ActionResult<LoginResponseDTO>> Login(LoginDTO loginDto) =>
        CreatedAtAction(nameof(Login), await _authService.Login(loginDto));

    [HttpPost("SwitchServer")]
    [Authorize]
    public async Task<ActionResult<string>> SwitchServer(SwitchServerDTO switchServerDto)
    {
        var userService = HttpContext.RequestServices.GetService<UserService>();
        if (userService == null) throw new BadHttpRequestException(ExceptionMessage.SomethingWentWrong);

        var userId = userService.GetUserIdFromHttpContext(HttpContext);
        
        return CreatedAtAction(nameof(SwitchServer), await _authService.SwitchServer(switchServerDto, userId));
    }
        
}