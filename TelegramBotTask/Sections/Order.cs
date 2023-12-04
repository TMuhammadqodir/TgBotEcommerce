using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotTask.DbContexts;

namespace TelegramBotTask.Sections;

public class Order
{
    private readonly ITelegramBotClient _botClient;
    private readonly AppDbContext _dbContext;
    public Order(ITelegramBotClient botClient)
    {
        _botClient = botClient;
        _dbContext = new AppDbContext();
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

        await _botClient.SendTextMessageAsync(message.Chat.Id,
                                            "Buyurtmani o'zingiz olib keting, " +
                                            "yoki Yetkazib berishni tanlang",
                                             replyMarkup: replyKeyboard);
    }

    public async Task Delivery(Message message)
    {

        ReplyKeyboardMarkup replyKeyboard = new(new[]
        {
            new[]{
                new KeyboardButton("Eng yaqin filialni aniqlash"),
            },
            new[]{
                new KeyboardButton("⬅️ Ortga"),
            }
        })
        {
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(message.Chat.Id,
                                            "Buyurtmangizni qayerga yetkazib berish kerak 🚙?",
                                             replyMarkup: replyKeyboard);
    }

    public async Task TakeAway(Message message)
    {

        ReplyKeyboardMarkup replyKeyboard = new(new[]
        {
            new[]
            {
                new KeyboardButton("Eng yaqin filialni aniqlash")
            },
            new[]
            {
                new KeyboardButton("Novza"),
                new KeyboardButton("Sum")
            },
            new[]
            {
                new KeyboardButton("Gidrometsentr"),
                new KeyboardButton("Sergeli")
            },
            new[]
            {
                new KeyboardButton("Kukcha")
            },
        })
        {
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(message.Chat.Id,
                                             "Siz qayerda joylashgansiz 👀?" +
                                             "\nAgar lokatsiyangizni jo'natsangiz 📍," +
                                             " sizga yaqin bo'lgan filialni aniqlaymiz",
                                             replyMarkup: replyKeyboard);
    }

    public async Task SelectCategories(Message message)
    {
        var categories = _dbContext.Categories.ToList();

        var categoryButtons = categories
            .Select(category => new KeyboardButton(category.Name))
            .ToArray();

        var additionalButtons = new[]
        {
            new KeyboardButton("📥 Savat"),
            new KeyboardButton("🚖 Buyurtuma berish")
        };

        var backButtons = new[]
        {
            new KeyboardButton("⬅️ Ortga")
        };

        var allButtons = additionalButtons.Concat(categoryButtons).Concat(backButtons).ToArray();

        var rows = SplitIntoRows(allButtons, 2);

        var replyKeyboard = new ReplyKeyboardMarkup(rows)
        {
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(message.Chat.Id,
                                             "Nimadan boshlaymiz?",
                                             replyMarkup: replyKeyboard);
    }

    public async Task SelectProducts(Message message, string categoryName)
    {
        var category = _dbContext.Categories.FirstOrDefault(c => c.Name.ToLower().Equals(categoryName.ToLower()));

        if (category is null)
            return;

        var products = _dbContext.Products.Where(p=> p.CategoryId.Equals(category.Id)).ToList();

        var productButtons = products
            .Select(product => new KeyboardButton(product.Name))
            .ToArray();

        var additionalButtons = new[]
        {
            new KeyboardButton("⬅️ Ortga"),
            new KeyboardButton("📥 Savat"),
        };

        var allButtons = additionalButtons.Concat(productButtons).ToArray();

        var rows = SplitIntoRows(allButtons, 2);

        var replyKeyboard = new ReplyKeyboardMarkup(rows)
        {
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(message.Chat.Id,
                                             "Nimadan buyurtma qilasiz?",
                                             replyMarkup: replyKeyboard);
    }

    public async Task SelectQuantity(Message message, string productName)
    {
        ReplyKeyboardMarkup replyKeyboard = new(new[]
        {
            new[]{
                new KeyboardButton("1"),
                new KeyboardButton("2"),
                new KeyboardButton("3"),
            },
            new[]{                
                new KeyboardButton("4"),
                new KeyboardButton("5"),
                new KeyboardButton("6"),
            },
            new[]
            {
                new KeyboardButton("7"),
                new KeyboardButton("8"),
                new KeyboardButton("9"),
            },
            new[]
            {
            new KeyboardButton("📥 Savat"),
            new KeyboardButton("⬅️ Ortga"),
            },
        })
        {
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(message.Chat.Id,
                                            "Miqdorini tanlang yoki kiriting:",
                                             replyMarkup: replyKeyboard);
    }

    private KeyboardButton[][] SplitIntoRows(KeyboardButton[] buttons, int buttonsPerRow)
    {
        var rows = new List<KeyboardButton[]>();
        for (int i = 0; i < buttons.Length; i += buttonsPerRow)
        {
            var row = buttons.Skip(i).Take(buttonsPerRow).ToArray();
            rows.Add(row);
        }
        return rows.ToArray();
    }
}
