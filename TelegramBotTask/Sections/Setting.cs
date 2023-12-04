using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotTask.Sections;

internal class Setting
{
    private readonly ITelegramBotClient _botClient;
    public Setting(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task SelectSetting(Message message)
    {

        ReplyKeyboardMarkup replyKeyboard = new(new[]
        {
            new[]{
                new KeyboardButton("Isimni o'zgartirish"),
                new KeyboardButton("Raqamni o'zgartirish"),
            },
            new[]{
                new KeyboardButton("⬅️ Ortga"),
            }
        })
        {
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(message.Chat.Id, 
                                             "⚙️ Sozlamalar", 
                                             replyMarkup: replyKeyboard);
    }
}
