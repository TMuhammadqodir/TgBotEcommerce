using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotTask.DbContexts;
using Microsoft.EntityFrameworkCore;
using TelegramBotTask.Models;

namespace TelegramBotTask.Sections;

public class Info
{
    private readonly Menu _menu;
    private readonly AppDbContext _dbContext;
    private readonly ITelegramBotClient _botClient;
    public Info(ITelegramBotClient botClient)
    {
        _botClient = botClient;
        _menu = new Menu(botClient);
        _dbContext = new AppDbContext();
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

        var roadWays = await _dbContext.RoadWays.Where(r=> r.ChatId.Equals(message.Chat.Id)).ToListAsync();
        
        if (roadWays is null || roadWays.Count ==0)
        {
            var roadWay = new RoadWay()
            {
                Name = message.Text,
                ChatId = message.Chat.Id,
            };

            _dbContext.RoadWays.Add(roadWay);

            await _botClient.SendTextMessageAsync(message.Chat.Id,
                         "Qaysi shahobchani tanlaysiz?",
                          replyMarkup: replyKeyboard);
        }
        else if (message.Text.Equals("Kukcha"))
        {
            await SendInfo(message);
        }
        else if (message.Text.Equals("⬅️ Ortga"))
        {
            _dbContext.RoadWays.Remove(roadWays[0]);
            await _menu.ShowSections(message);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task SendInfo(Message message)
    {
        await _botClient.SendTextMessageAsync(message.Chat.Id, 
            "https://maps.google.com/maps?q=41.322653,69.199105&ll=41.322653,69.199105&z=16");
    }
}
