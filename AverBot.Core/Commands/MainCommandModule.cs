using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace AverBot.Core.Commands;

public class MainCommandModule : ModuleBase<SocketCommandContext>
{
    public SocketGuildUser? CurrentUser => (SocketGuildUser?)Context.User;
    public async Task<IUser> GetRandomUser(SocketUserMessage message)
    {
        var rnd = new Random();

        var usersCollections = await message.Channel.GetUsersAsync().ToListAsync();
        var users = usersCollections.SelectMany(usersCollection => usersCollection).ToList();

        return users[rnd.Next(0, users.Count)];
    }
}