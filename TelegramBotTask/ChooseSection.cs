using System.Net;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotTask.DbContexts;
using TelegramBotTask.Sections;
using TelegramBotTask.Models;
using System.Threading;

namespace TelegramBotTask;

public class ChooseSection
{
    private readonly Menu _menu;
    private readonly Info _info;
    private readonly Order _order;
    private readonly Setting _setting;
    private readonly Comment _comment;
    private readonly Entrance _entrance;
    private readonly AppDbContext _dbContext;
    private readonly TelegramBotClient _client;
    private bool check = false;
    public ChooseSection()
    {
        Console.WriteLine("Telegram bot started");

        _dbContext = new AppDbContext();
        _client = new TelegramBotClient("6155300733:AAEpsRnznvvI_nBIAHWbHOjG2YjaXXqcjng");
        _client.StartReceiving(Update, Error);
        _menu = new Menu(_client);
        _info = new Info(_client);
        _order = new Order(_client);
        _setting = new Setting(_client);
        _comment = new Comment(_client);
        _entrance = new Entrance(_client);

        Console.WriteLine("Any keyboard stop telegram bot");
        Console.ReadKey();
    }

    async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        if (update.Message is not { } message)
            return;

        if (check)
        {
            if (message.Text is not { } messageText)
                return;

            await _entrance.WriteFirstAndLastName(message);

            check = false;
        }

        if (message.Text is not null)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.ChatId.Equals(message.Chat.Id));

            if (user is null)
            {
                await _entrance.SendPhoneNumber(message.Chat.Id);
            }
            else
            {
                string messageText = message.Text;

                switch (messageText)
                {
                    case "🛍 Buyurtma berish":
                        await _order.SelectCategories(message);
                        break;

                    case "✍️ Fikr bildirish":
                        await _comment.SelectThought(message);
                        break;

                    case "☎️ Biz bilan aloqa":
                        Message sendMessage = await botClient.SendTextMessageAsync( chatId: message.Chat.Id,
                                                                                    text: "Agar sizda savollar bo'lsa bizga " +
                                                                                    "telefon qilishingiz mumkin: +998 95-115-44-30"
                        );
                        break;

                    case "ℹ️ Ma'lumot":
                        await _info.SelectInfo(message);
                        break;

                    case "⚙️ Sozlamalar":
                        await _setting.SelectSetting(message);
                        break;

                    default:
                        await _menu.ShowSections(message); 
                        break;
                }
            }
        }
        else if (message.Contact is not null)
        {
            await _entrance.EnterFirstAndLastName(message);
            check = true;
        }
    }


    Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}
