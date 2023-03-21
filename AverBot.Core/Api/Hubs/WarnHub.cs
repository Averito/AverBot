using AverBot.Core.DTO;
using AverBot.Core.Domain.Entities;
using AverBot.Core.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;

namespace AverBot.Core.Hubs;

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