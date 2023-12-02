using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using Microsoft.EntityFrameworkCore;
using TelegramBotTask.Models;
using TelegramBotTask.DbContexts;

namespace TelegramBotTask.Sections;

public class Entrance
{
    private readonly ITelegramBotClient _botClient;
    private readonly AppDbContext _dbContext;
    public Entrance(ITelegramBotClient botClient)
    {
        _botClient = botClient;
        _dbContext = new AppDbContext();
    }

    public async Task SendPhoneNumber(long chatId)
    {

        ReplyKeyboardMarkup replyKeyboard = new(new[]
        {
            KeyboardButton.WithRequestContact("📱 Raqamni jo'natish")

        })
        {
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(chatId, "📱 Telefon raqamingiz qanday? " +
                                                       "Telefon raqamingizni jo'natish uchun, " +
                                                       "quidagi \"📱 Raqamni jo'natish\" " +
                                                       "tugmasini bosing", 
                                                       replyMarkup: replyKeyboard);
    }

    public async Task EnterFirstAndLastName(Message message)
    {
        var telNumber = message.Contact.PhoneNumber;

        var userCreation = new UserInformation()
        {
            ChatId = message.Chat.Id,
            FirstName = message.Chat.FirstName,
            LastName = message.Chat.LastName,
            Username = message.Chat.Username,
            TelNumber = telNumber,
        };

        _dbContext.Users.Add(userCreation);
        await _dbContext.SaveChangesAsync();

        Message sendMessage = await _botClient.SendTextMessageAsync(
           chatId: message.Chat.Id,
           text: "Ism sharifingizni kiriting",
           replyMarkup: new ReplyKeyboardRemove()
           );
    }

    public async Task WriteFirstAndLastName(Message message)
    {
        var user = _dbContext.Users.FirstOrDefault(user => user.ChatId.Equals(message.Chat.Id));

        if (user is null) return;

        user.Name = message.Text;
        await _dbContext.SaveChangesAsync();
    }
}
