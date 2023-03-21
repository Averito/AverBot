using Discord.Commands;

namespace AverBot.Core.Infrastructure.Commands;

public class SelectCommand : ModuleBase<SocketCommandContext>
{
    [Command("выбери")]
    public async Task SelectOneVariant([Remainder] string input)
    {
        var rnd = new Random();
        var variables = input.Split("или");
    
        var answer = variables[rnd.Next(0, variables.Length)].Replace("?", "");
        await ReplyAsync(answer);
    }
}