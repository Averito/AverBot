using System.Security.Claims;
using AverBot.API.DTO;
using AverBot.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AverBot.API.Controllers;

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
}