using Discord;
using Discord.Commands;

namespace AverBot.Core.Commands;

public class FactorialCommand: MainCommandModule
{
    [Command("факториал")]
    public async Task CalculateFactorial(string input)
    {
        if (CurrentUser == null) return;
        
        int num = 0;
        var isNum = int.TryParse(input, out num);

        if (!isNum)
        {
            await ReplyAsync("Отправь число паже");
            return;
        }

        try
        {
            checked
            {
                ulong result = 1;
                for (int i = 1; i <= num; i++) result *= Convert.ToUInt64(i);
                await ReplyAsync(result.ToString());
            }
        }
        catch (OverflowException exception)
        {
            var embed = new EmbedBuilder()
                .WithTitle($"Ошибка: {exception.Message}")
                .WithAuthor(CurrentUser.ToString(), CurrentUser.GetAvatarUrl() ?? CurrentUser.GetDefaultAvatarUrl())
                .WithDescription("Отправьте число по меньше")
                .WithCurrentTimestamp()
                .Build();
            
            await ReplyAsync(embed: embed);
        }
    }
}