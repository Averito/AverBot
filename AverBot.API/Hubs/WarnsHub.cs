using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AverBot.API.Hubs;

[Authorize]
public class WarnsHub : Hub
{
    public async Task Send(string message)
    {
        await Clients.All.SendAsync("send", message);
    }
}