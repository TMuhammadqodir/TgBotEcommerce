using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotTask.Sections;

public class Info
{
    private readonly ITelegramBotClient _botClient;
    public Info(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task SelectInfo(Message message)
    {

        ReplyKeyboardMarkup replyKeyboard = new(new[]
       {
            new[]
            {
                new KeyboardButton("Kukcha")
            },
            new[]
            {
                new KeyboardButton("⬅️ Ortga")
            },
        })
        {
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(message.Chat.Id, "Qaysi shahobchani tanlaysiz?",
                                                       replyMarkup: replyKeyboard);
    }
}
