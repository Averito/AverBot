using AverBot.Core.Attributes;
using Discord;
using Discord.WebSocket;

namespace AverBot.Core.Commands;

public class GeneralCommands
{
    public string Name { get; }
    private Random rnd { get; } = new Random();
    private DiscordSocketClient _client { get; }
    
    public GeneralCommands(DiscordSocketClient client)
    {
        _client = client;
        Name = "General";
    }

    [SlashCommand("get-me", "Получить себя :D")]
    public Task GetMyName(SocketSlashCommand command)
    {
        command.RespondAsync($"{command.User.Username}#{command.User.Discriminator}");
        return Task.CompletedTask;
    }

    [Command("мнение")]
    public Task GetChance(SocketMessage message)
    {
        message.Channel.SendMessageAsync($"Я думаю процентов на {rnd.Next(0, 100)}%");
        
        return Task.CompletedTask;
    }

    [Command("выбери")]
    public Task SelectOneVariant(SocketMessage message)
    {
        var variables = message.Content.Substring(7).Split("или");

        var answer = variables[rnd.Next(0, 2)].Replace("?", "");
        message.Channel.SendMessageAsync(answer);
        
        return Task.CompletedTask;
    }

    [Command("шар")]
    public async Task Devine(SocketMessage message)
    {
        if (string.IsNullOrEmpty(GetRequest(message.Content)))
        {
            await message.Channel.SendMessageAsync("А не пойти ка ли тебе куда-нибудь за идеей для меня?");
            return;
        }
        var randomUser = await GetRandomUser(message);
        
        var devineVariants = new [] { "Гадаю на кофейной гуще...", $"Гадаю на жопе {message.Author.Username}", "Обращаюсь к вселенной...", $"Спрашиваю у {randomUser.Username}..." };
        var devineVariant = devineVariants[rnd.Next(0, devineVariants.Length)];

        var answerVariants = new [] { "Думаю да", "Думаю нет", "Не определился кароч", "Вселенная сказала иди нахуй", "Да блэа, подумай сам", $"Подышав маткой я принял решение - Да" };
        var answerVariant = answerVariants[rnd.Next(0, answerVariants.Length)];
        
        await message.Channel.SendMessageAsync(devineVariant);
        await Task.Delay(200);
        await message.Channel.SendMessageAsync(answerVariant);
    }

    private string GetRequest(string message)
    {
        var splittedMessage = message.Split(" ");
        return String.Join(" ", splittedMessage.Skip(1));
    }

    private async Task<IUser> GetRandomUser(SocketMessage message)
    {
        var usersEnumerable = await message.Channel.GetUsersAsync().ToArrayAsync();
        var users = new List<IUser>();

        foreach (var usersCollection in usersEnumerable)
        {
            foreach (var user in usersCollection)
            {
                users.Add(user);
            }
        }

        return users[rnd.Next(0, users.Count)];
    }
}