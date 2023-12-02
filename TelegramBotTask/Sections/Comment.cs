using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotTask.Sections;

public class Comment
{
    private readonly ITelegramBotClient _botClient;
    public Comment(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task SelectThought(Message message)
    {

        ReplyKeyboardMarkup replyKeyboard = new(new[]
        {
            new[]{
                new KeyboardButton("Hammasi yoqdi ♥️"),
            },
            new[]{
                new KeyboardButton("Yaxshi ⭐️⭐️⭐️⭐️"),
            },
            new[]{
                new KeyboardButton("Yoqmadi ⭐️⭐️⭐️"),
            },
            new[]{
                new KeyboardButton("Yomon ⭐️⭐️"),
            },
            new[]{
                new KeyboardButton("Juda yomon 👎🏻"),
            },
            new[]{
                new KeyboardButton("⬅️ Ortga"),
            },
        })
        {
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(message.Chat.Id, "Fish and Breadni tanlaganingiz uchun rahmat." +
                                                      "\nAgar siz bizning xizmat sifatimizni yaxshilashimizga " +
                                                      "yordam bersangiz hursand bulardik." +
                                                      "\nBuning uchun 5 bal tizim asosida baholang", 
                                                      replyMarkup: replyKeyboard);
    }
}
