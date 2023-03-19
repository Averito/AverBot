using Microsoft.AspNetCore.SignalR;

namespace AverBot.API.Hubs;

public class MainHub<T> : Hub
{
    protected readonly ILogger<T> _logger;

    public MainHub(ILogger<T> logger)
    {
        _logger = logger;
    }
}