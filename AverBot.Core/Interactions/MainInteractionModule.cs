using Discord.Interactions;
using Discord.WebSocket;

namespace AverBot.Core.Interactions;

public class MainInteractionModule : InteractionModuleBase<SocketInteractionContext>
{
    public SocketGuildUser? CurrentUser => (SocketGuildUser?)Context.User;
}
