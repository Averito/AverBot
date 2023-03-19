using AverBot.API.DTO;
using AverBot.API.Models;
using AverBot.API.Services;
using Microsoft.AspNetCore.Authorization;

namespace AverBot.API.Hubs;

[Authorize]
public class WarnHub : MainHub<WarnHub>
{
    private readonly WarnService _warnService;
    
    public WarnHub(ILogger<WarnHub> logger, WarnService warnService) : base(logger)
    {
        _warnService = warnService;
    }

    public async Task<Warn> Create(CreateWarnDTO createWarnDto)
    {
        return await _warnService.Create(Clients, createWarnDto);
    }
}