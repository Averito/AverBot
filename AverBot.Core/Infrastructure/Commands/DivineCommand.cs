using Discord.Commands;

namespace AverBot.Core.Infrastructure.Commands;

public class DivineCommand : MainCommandModule
{
    private readonly string[] _staticDevineVariants = { "Гадаю на кофейной гуще...", "ЕЖЕ...", "Обращаюсь к вселенной..." };
    private readonly string[] _staticAnswerVariants =
    {
        "Думаю да", "Ты жук", "Думаю нет", "Не определился кароч", "Вселенная сказала иди нахуй", "Да блэа, подумай сам", "Подышав маткой я принял решение - Да"
    };
    
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
        
        var devineVariants = new [] { $"Гадаю на жопе {message.Author.Username}", $"Спрашиваю у {randomUser.Username}..." };
        var joinedDevineVariants = devineVariants.Concat(_staticDevineVariants).ToArray();
        var devineVariant = joinedDevineVariants[rnd.Next(0, devineVariants.Length)];
    
        var answerVariants = new [] { $"{randomUser.Username} сказал да", $"{randomUser.Username} сказал нет" };
        var joinedAnswersVariants = answerVariants.Concat(_staticAnswerVariants).ToArray();
        var answerVariant = joinedAnswersVariants[rnd.Next(0, joinedAnswersVariants.Length)];
        
        await ReplyAsync(devineVariant);
        await Task.Delay(200);
        await ReplyAsync(answerVariant);
    }
}