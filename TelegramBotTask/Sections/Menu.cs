using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Runtime.CompilerServices;

namespace TelegramBotTask.Sections;

public class Menu
{
    private readonly ITelegramBotClient _botClient;
    public Menu(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }
    public async Task ShowSections(Message message)
    {

        ReplyKeyboardMarkup replyKeyboard = new(new[]
        {
            new[]
            {
                new KeyboardButton("🛍 Buyurtma berish")
            },
            new[]
            {
                new KeyboardButton("✍️ Fikr bildirish"),
                new KeyboardButton("☎️ Biz bilan aloqa")
            },
            new[]
            {
                new KeyboardButton("ℹ️ Ma'lumot"),
                new KeyboardButton("⚙️ Sozlamalar")
            },
        }){
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(message.Chat.Id, 
                                             "Juda yaxshi birgalikda buyurtma beramizmi? 😃",
                                             replyMarkup: replyKeyboard);
    }

    public async Task SelectSection(Message message)
    {

    }
}
