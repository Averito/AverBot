using Discord.Commands;

namespace AverBot.Core.Commands;

public class DivineCommand : MainCommandModule
{
    [Command("шар")]
    public async Task Devine([Remainder] string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            await ReplyAsync("А не пойти ка ли тебе куда-нибудь за идеей для меня?");
            return;
        }
    
        var rnd = new Random();
        var message = Context.Message;
        var randomUser = await GetRandomUser(message);
        
        var devineVariants = new [] { "Гадаю на кофейной гуще...", $"Гадаю на жопе {message.Author.Username}", "Обращаюсь к вселенной...", $"Спрашиваю у {randomUser.Username}..." };
        var devineVariant = devineVariants[rnd.Next(0, devineVariants.Length)];
    
        var answerVariants = new [] { "Думаю да", "Думаю нет", "Не определился кароч", "Вселенная сказала иди нахуй", "Да блэа, подумай сам", $"Подышав маткой я принял решение - Да" };
        var answerVariant = answerVariants[rnd.Next(0, answerVariants.Length)];
        
        await message.Channel.SendMessageAsync(devineVariant);
        await Task.Delay(200);
        await message.Channel.SendMessageAsync(answerVariant);
    }
}