using Discord;
using Discord.WebSocket;

namespace AverBot.Core.Handlers;

public class UserService
{
    public async Task<IUser> GetRandomUser(SocketMessage message)
    {
        var rnd = new Random();

        var usersCollections = await message.Channel.GetUsersAsync().ToListAsync();
        var users = usersCollections.SelectMany(usersCollection => usersCollection).ToList();

        return users[rnd.Next(0, users.Count)];
    }
}