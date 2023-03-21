using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace AverBot.Core.Infrastructure.Interactions;

public class MainInteractionModule : InteractionModuleBase<SocketInteractionContext>
{
    public SocketGuildUser? CurrentUser => (SocketGuildUser?)Context.User;

    public async Task SendErrorMessage(string text)
    {
        var embed = new EmbedBuilder()
            .WithAuthor(CurrentUser.ToString(), CurrentUser.GetAvatarUrl() ?? CurrentUser.GetDefaultAvatarUrl())
            .WithTitle(text)
            .WithColor(Color.Red)
            .WithCurrentTimestamp()
            .Build();

        await RespondAsync(embed: embed, ephemeral: true);
    }
    
    public async Task SendNotificationMessage(string title)
    {
        var embed = new EmbedBuilder()
            .WithTitle(title)
            .WithColor(Color.Blue)
            .WithCurrentTimestamp()
            .Build();
        
        await Context.Channel.SendMessageAsync(embed: embed);
    }

    public Embed GetSuccessMessage(string title, string? description = null, SocketGuildUser? author = null, Color? color = null)
    {
        var authorUser = author;
        if (author == null) authorUser = CurrentUser;

        var embed = new EmbedBuilder()
            .WithAuthor(authorUser?.ToString(), authorUser?.GetAvatarUrl() ?? authorUser?.GetDefaultAvatarUrl())
            .WithTitle(title)
            .WithColor(color ?? Color.Green)
            .WithCurrentTimestamp();
            
        if (description != null) embed.WithDescription(description);

        return embed.Build();
    }
}
