using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.EntityFrameworkCore;
using TelegramBotTask.Models;
using TelegramBotTask.DbContexts;

namespace TelegramBotTask.Sections;

public class Comment
{
    private readonly Menu _menu;
    private readonly AppDbContext _dbContext;
    private readonly ITelegramBotClient _botClient;
    public Comment(ITelegramBotClient botClient)
    {
        _botClient = botClient;
        _menu = new Menu(botClient);
        _dbContext = new AppDbContext();
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
        
        var roadWays = await _dbContext.RoadWays.Where(r => r.ChatId.Equals(message.Chat.Id)).ToListAsync();

        if (roadWays is null || roadWays.Count == 0)
        {
            var roadWay = new RoadWay()
            {
                Name = message.Text,
                ChatId = message.Chat.Id,
            };

            _dbContext.RoadWays.Add(roadWay);

            await _botClient.SendTextMessageAsync(message.Chat.Id,
                                                          "Fish and Breadni tanlaganingiz uchun rahmat." +
                                                          "\nAgar siz bizning xizmat sifatimizni yaxshilashimizga " +
                                                          "yordam bersangiz hursand bulardik." +
                                                          "\nBuning uchun 5 bal tizim asosida baholang",
                                                          replyMarkup: replyKeyboard);
        }
        else if (message.Text.Equals("Hammasi yoqdi ♥️")
                || message.Text.Equals("Yaxshi ⭐️⭐️⭐️⭐️")
                || message.Text.Equals("Yoqmadi ⭐️⭐️⭐️")
                || message.Text.Equals("Yomon ⭐️⭐️")
                || message.Text.Equals("Juda yomon 👎🏻"))
        {
            await SendMessage(message);
        }
        else if (message.Text.Equals("⬅️ Ortga"))
        {
            _dbContext.RoadWays.Remove(roadWays[0]);
            await _menu.ShowSections(message);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task SendMessage(Message message)
    {
        await _botClient.SendTextMessageAsync(message.Chat.Id,
            "O'z fikr va mulohazalaringizni jo'nating.");
    }
}
