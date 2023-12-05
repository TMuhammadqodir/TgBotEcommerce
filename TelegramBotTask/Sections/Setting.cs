using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotTask.DbContexts;
using Microsoft.EntityFrameworkCore;
using TelegramBotTask.Models;

namespace TelegramBotTask.Sections;

internal class Setting
{
    private readonly Menu _menu;
    private readonly AppDbContext _dbContext;
    private readonly ITelegramBotClient _botClient;
    public Setting(ITelegramBotClient botClient)
    {
        _botClient = botClient;
        _menu = new Menu(botClient);
        _dbContext = new AppDbContext();
    }

    public async Task ShowSetting(Message message)
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

    public async Task SelectSetting(Message message)
    {
        var roadWays = await _dbContext.RoadWays.Where(r => r.ChatId.Equals(message.Chat.Id)).ToListAsync();

        if (roadWays is null || roadWays.Count == 0)
        {
            var roadWay = new RoadWay()
            {
                Name = message.Text,
                ChatId = message.Chat.Id,
            };

            _dbContext.RoadWays.Add(roadWay);

            await ShowSetting(message);
        }
        else if(roadWays.Count == 2)
        {
            if (roadWays[1].Name.Equals("Isimni o'zgartirish"))
                await ChangeName(message);
            else
                await ChangeTelNumber(message);
        }
        else if (message.Text.Equals("Isimni o'zgartirish"))
        {
            await ChangeName(message);
        }
        else if(message.Text.Equals("Raqamni o'zgartirish"))
        {
            await ChangeTelNumber(message);
        }
        else if (message.Text.Equals("⬅️ Ortga"))
        {
            _dbContext.RoadWays.Remove(roadWays[0]);
            await _menu.ShowSections(message);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task ChangeName(Message message)
    {
        ReplyKeyboardMarkup replyKeyboard = new(new[]
        {
            new[]{
                new KeyboardButton("⬅️ Ortga"),
            }
        })
        {
            ResizeKeyboard = true
        };

        var roadWays = await _dbContext.RoadWays.Where(r => r.ChatId.Equals(message.Chat.Id)).ToListAsync();

        if (roadWays.Count < 2)
        {
            var roadWay = new RoadWay()
            {
                Name = message.Text,
                ChatId = message.Chat.Id,
            };

            _dbContext.RoadWays.Add(roadWay);

            await _botClient.SendTextMessageAsync(message.Chat.Id,
                                                  "Ismingizni kiriting",
                                                  replyMarkup: replyKeyboard);
        }
        else if (message.Text.Equals("⬅️ Ortga"))
        {
            _dbContext.RoadWays.Remove(roadWays[1]);
            await ShowSetting(message);
        }
        else
        {
            var user = _dbContext.Users.FirstOrDefault(u=> u.ChatId.Equals(message.Chat.Id));
            user.Name = message.Text;
            _dbContext.RoadWays.Remove(roadWays[1]);
            await ShowSetting(message);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task ChangeTelNumber(Message message)
    {
        ReplyKeyboardMarkup replyKeyboard = new(new[]
{
            new[]{
                new KeyboardButton("⬅️ Ortga"),
            }
        })
        {
            ResizeKeyboard = true
        };

        var roadWays = await _dbContext.RoadWays.Where(r => r.ChatId.Equals(message.Chat.Id)).ToListAsync();

        if (roadWays.Count < 2)
        {
            var roadWay = new RoadWay()
            {
                Name = message.Text,
                ChatId = message.Chat.Id,
            };

            _dbContext.RoadWays.Add(roadWay);

            await _botClient.SendTextMessageAsync(message.Chat.Id,
                                             "📱 Raqamni +998 ** *** ** ** shakilda yuboring.",
                                             replyMarkup: replyKeyboard);
        }
        else if (message.Text.Equals("⬅️ Ortga"))
        {
            _dbContext.RoadWays.Remove(roadWays[1]);
            await ShowSetting(message);
        }
        else
        {
            var isValidTelNumber = await CheckTelNumber(message.Text); 

            if(!isValidTelNumber)
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id,
                                 "Iltimos raqamni namunadagidek kiriting! " +
                                 "Namuna: +998 ** *** ** **");
            }
            else
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.ChatId.Equals(message.Chat.Id));
                user.TelNumber = message.Text;
                _dbContext.RoadWays.Remove(roadWays[1]);
                await ShowSetting(message);
            }
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> CheckTelNumber(string telNumber)
    {
        if (telNumber.Length != 13)
            return false;

        if (!telNumber[..4].Equals("+998"))
            return false;

        for (int i = 4; i < telNumber.Length; i++)
            if (48 > (int)telNumber[i] || 57 < (int)telNumber[i])
                return false;

        return true;
    }
}
