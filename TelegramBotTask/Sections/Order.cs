using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotTask.Sections;

public class Order
{
    private readonly ITelegramBotClient _botClient;
    public Order(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task SelectMethod(Message message)
    {

        ReplyKeyboardMarkup replyKeyboard = new(new[]
        {
            new[]{
                new KeyboardButton("🚖 Yetkazib berish"),
                new KeyboardButton("🏃 Olib ketish"),
            },
            new[]{
                new KeyboardButton("⬅️ Ortga"),
            }
        })
        {
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(message.Chat.Id, "Buyurtmani o'zingiz olib keting, " +
                                                               "yoki Yetkazib berishni tanlang", 
                                                                replyMarkup: replyKeyboard);
    }
}
