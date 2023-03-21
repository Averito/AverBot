using System.Reflection;
using Discord.Interactions;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace AverBot.Core.Infrastructure.Handlers;

public class InteractionsHandler
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _interaction;
    private readonly ServiceProvider _services;
    
    public InteractionsHandler(DiscordSocketClient client, InteractionService interaction, ServiceProvider services)
    {
        _client = client;
        _interaction = interaction;
        _services = services;
    }

    public async Task InitializeAsync()
    {
        await _interaction.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        _client.InteractionCreated += InteractionCreatedHandler;
    }

    private async Task InteractionCreatedHandler(SocketInteraction interaction)
    {
        try
        {
            var context = new SocketInteractionContext(_client, interaction);
            await _interaction.ExecuteCommandAsync(context, _services);
        }
        catch(Exception exception)
        {
            var json = JsonConvert.SerializeObject(exception, Formatting.Indented);
            Console.WriteLine(json);
        }

    }
}