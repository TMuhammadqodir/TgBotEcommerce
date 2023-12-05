using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotTask.DbContexts;
using TelegramBotTask.Models;
using Microsoft.EntityFrameworkCore;

namespace TelegramBotTask.Sections;

public class Order
{
    private readonly Menu _menu;
    private readonly AppDbContext _dbContext;
    private readonly ITelegramBotClient _botClient;
    public Order(ITelegramBotClient botClient)
    {
        _botClient = botClient;
        _menu = new Menu(botClient);
        _dbContext = new AppDbContext();
    }

    public async Task ShowMethod(Message message)
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

    public async Task SelectMethod(Message message)
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

            await ShowMethod(message);
        }
        else if (roadWays.Count > 1)
        {
            if (roadWays[1].Name.Equals("🚖 Yetkazib berish"))
                await Delivery(message);
            else
                await SelectBranch(message);
        }
        else if (message.Text.Equals("🚖 Yetkazib berish"))
        {
            await Delivery(message);
        }
        else if (message.Text.Equals("🏃 Olib ketish"))
        {
            await SelectBranch(message);
        }
        else if (message.Text.Equals("⬅️ Ortga"))
        {
            _dbContext.RoadWays.Remove(roadWays[0]);
            await _menu.ShowSections(message);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task ShowDelivery(Message message)
    {
        ReplyKeyboardMarkup replyKeyboard = new(new[]
        {
            new[]{
                KeyboardButton.WithRequestLocation("Eng yaqil filialni aniqlash"),
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

    public async Task Delivery(Message message)
    {

        var roadWays = await _dbContext.RoadWays.Where(r => r.ChatId.Equals(message.Chat.Id)).ToListAsync();

        if (roadWays.Count < 2)
        {
            var roadWay = new RoadWay()
            {
                Name = message.Text,
                ChatId = message.Chat.Id,
            };

            _dbContext.RoadWays.Add(roadWay);

            await ShowDelivery(message);
        }
        else if (message.Location is not null || roadWays.Count == 3)
        {
            await SendConfirm(message);
        }
        else if (message.Text.Equals("⬅️ Ortga"))
        {
            _dbContext.RoadWays.Remove(roadWays[1]);
            await ShowMethod(message);
        }

        await _dbContext.SaveChangesAsync();
    }


    public async Task ShowConfirm(Message message, string text)
    {
        ReplyKeyboardMarkup replyKeyboard = new(new[]
{
            new[]{
                KeyboardButton.WithRequestLocation("Joylashuvni qayta jo'natish"),
            },
            new[]{
                new KeyboardButton("Tasdiqlash"),
            },
            new[]{
                new KeyboardButton("⬅️ Ortga"),
            }
        })
        {
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(message.Chat.Id,
                                text, replyMarkup: replyKeyboard);
    }

    public async Task SendConfirm(Message message)
    {
        var roadWays = await _dbContext.RoadWays.Where(r => r.ChatId.Equals(message.Chat.Id)).ToListAsync();

        if (roadWays.Count < 3)
        {
            var roadWay = new RoadWay()
            {
                Name = "Joylashuv",
                ChatId = message.Chat.Id,
            };

            _dbContext.RoadWays.Add(roadWay);
            string text = "Manzilingizni tekshiring!"
                          + "\nManzil natugrimi?" 
                          + "\nQayta jo'nating";
            await ShowConfirm(message, text);
        }
        else if (message.Location is not null)
        {
            _dbContext.RoadWays.Remove(roadWays[2]);
            string text = "Manzilingizni tekshiring!"
                          + "\nManzil natugrimi?"
                          + "\nQayta jo'nating";
            await ShowConfirm(message, text);
        }
        else if (message.Text.Equals("Tasdiqlash"))
        {
            _dbContext.RoadWays.Remove(roadWays[2]);
            string text = "Buyurtmangiz tasdiqlandi";
            
            await ShowConfirm(message, text);
        }
        else if (message.Text.Equals("⬅️ Ortga"))
        {
            _dbContext.RoadWays.Remove(roadWays[2]);
            await ShowDelivery(message);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task ShowBranch(Message message)
    {

        ReplyKeyboardMarkup replyKeyboard = new(new[]
        {
            new[]
            {
                new KeyboardButton("Novza"),
            },
            new[]
            {
                new KeyboardButton("Sum"),
                new KeyboardButton("Gidrometsentr"),
            },
            new[]
            {
                new KeyboardButton("Sergeli"),
                new KeyboardButton("Kukcha")
            },
            new[]
            {
                new KeyboardButton("⬅️ Ortga"),
            },
        })
        {
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(message.Chat.Id,
                                             "Filiallardan birini tanlang",
                                             replyMarkup: replyKeyboard);
    }

    public async Task SelectBranch(Message message)
    {
        var roadWays = await _dbContext.RoadWays.Where(r => r.ChatId.Equals(message.Chat.Id)).ToListAsync();

        List<string> branches = new List<string>() { "Novza", "Sum", "Gidrometsentr","Sergeli", "Kukcha" };
                                                                                     
        if (roadWays.Count < 2)
        {
            var roadWay = new RoadWay()
            {
                Name = message.Text,
                ChatId = message.Chat.Id,
            };

            _dbContext.RoadWays.Add(roadWay);

            await ShowBranch(message);
        }
        else if (roadWays.Count > 2)
        {
            await SelectCategories(message);
        }
        else if (branches.Contains(message.Text))
        {
            await SelectCategories(message);
        }
        else if (message.Text.Equals("⬅️ Ortga"))
        {
            _dbContext.RoadWays.Remove(roadWays[1]);
            await ShowMethod(message);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task ShowCategories(Message message)
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

    public async Task SelectCategories(Message message)
    {
        var roadWays = await _dbContext.RoadWays.Where(r => r.ChatId.Equals(message.Chat.Id)).ToListAsync();

        var categories = _dbContext.Categories.ToList();

        if (roadWays.Count < 3)
        {
            var roadWay = new RoadWay()
            {
                Name = message.Text,
                ChatId = message.Chat.Id,
            };

            _dbContext.RoadWays.Add(roadWay);

            await ShowCategories(message);
        }
        else if(roadWays.Count > 3)
        {
            if (roadWays[3].Name.Equals("📥 Savat"))
                await ControllBasket(message);
            else
                await SelectProducts(message, message.Text);
        }
        else if (categories.Select(c => c.Name).Contains(message.Text))
        {
             await SelectProducts(message, message.Text);
        }
        else if(message.Text.Equals("📥 Savat"))
        {
            await ControllBasket(message);
        }
        else if (message.Text.Equals("⬅️ Ortga"))
        {
            _dbContext.RoadWays.Remove(roadWays[2]);
            await ShowBranch(message);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task ShowProducts(Message message, string categoryName)
    {
        var roadWays = await _dbContext.RoadWays.Where(r => r.ChatId.Equals(message.Chat.Id)).ToListAsync();

        if (roadWays.Count >= 4)
            categoryName = roadWays[3].Name;

        var category = _dbContext.Categories.FirstOrDefault(c => c.Name.ToLower().Equals(categoryName.ToLower()));

        var products = await _dbContext.Products.Where(p => p.CategoryId.Equals(category.Id)).ToListAsync();

        var productButtons = products
            .Select(product => new KeyboardButton(product.Name))
            .ToArray();

        var additionalButtons = new[]
        {
            new KeyboardButton("⬅️ Ortga")
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

    public async Task SelectProducts(Message message, string categoryName)
    {
        var roadWays = await _dbContext.RoadWays.Where(r => r.ChatId.Equals(message.Chat.Id)).ToListAsync();

        if(roadWays.Count >= 4)
             categoryName = roadWays[3].Name;
        

        var category = _dbContext.Categories.FirstOrDefault(c => c.Name.ToLower().Equals(categoryName.ToLower()));
        var products = _dbContext.Products.Where(p => p.CategoryId.Equals(category.Id)).ToList();

        if (roadWays.Count < 4)
        {
            var roadWay = new RoadWay()
            {
                Name = message.Text,
                ChatId = message.Chat.Id,
            };

            _dbContext.RoadWays.Add(roadWay);

            await ShowProducts(message, categoryName);
        }
        else if (products.Select(c => c.Name).Contains(message.Text) || roadWays.Count > 4)
        {
            await SelectQuantity(message, message.Text);
        }
        else if (message.Text.Equals("⬅️ Ortga"))
        {
            _dbContext.RoadWays.Remove(roadWays[3]);
            await ShowCategories(message);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task ShowQuantity(Message message, string productName)
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
    public async Task SelectQuantity(Message message, string productName)
    {
        var roadWays = await _dbContext.RoadWays.Where(r => r.ChatId.Equals(message.Chat.Id)).ToListAsync();

        if (roadWays.Count >= 5)
            productName = roadWays[4].Name;

        var product = _dbContext.Products.FirstOrDefault(c => c.Name.ToLower().Equals(productName.ToLower()));

        if (roadWays.Count < 5)
        {
            var roadWay = new RoadWay()
            {
                Name = message.Text,
                ChatId = message.Chat.Id,
            };

            _dbContext.RoadWays.Add(roadWay);

            await ShowQuantity(message, productName);
        }
        else if (message.Text.Equals("⬅️ Ortga"))
        {
            _dbContext.RoadWays.Remove(roadWays[4]);
            await ShowProducts(message, message.Text);
        }
        else
        {
            int number;

            if (int.TryParse(message.Text, out number))
            {
                var userBasket = new UserBasket()
                {
                    ChatId = message.Chat.Id,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = number,
                    TotalPrice = number * product.Price,
                };

                _dbContext.UserBaskets.Add(userBasket);
                _dbContext.RoadWays.Remove(roadWays[4]);
                _dbContext.RoadWays.Remove(roadWays[3]);

                await ShowCategories(message);
            }
            else
            {
                await ShowQuantity(message, message.Text);
            }
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task ShowBasket(Message message)
    {
        var products = await _dbContext.UserBaskets.Where(u=> u.ChatId.Equals(message.Chat.Id)).ToListAsync();

        var productButtons = products
            .Select(product => new KeyboardButton("❌ "+product.ProductName))
            .ToArray();

        var additionalButtons = new[]
        {
            new KeyboardButton("🔄 Tozalash"),
            new KeyboardButton("🚘 Buyurtma berish"),
            new KeyboardButton("⬅️ Ortga"),
        };

        var allButtons = productButtons.Concat(additionalButtons).ToArray();

        var rows = SplitIntoRows(allButtons, 1);

        var replyKeyboard = new ReplyKeyboardMarkup(rows)
        {
            ResizeKeyboard = true
        };

        await _botClient.SendTextMessageAsync(message.Chat.Id,
                                             "*«❌ Maxsulot nomi»* - savatdan o'chirish" 
                                             +"*«🔄 Tozalash»*-savatni butunlay bo'shatish",
                                             replyMarkup: replyKeyboard);

        var productNames = products.Select(p => p.ProductName).ToList();

        var sendInfo = "";

        for(int i=0; i< products.Count; i++)
        {
            sendInfo += productNames[i] + "\n";
            sendInfo += $"{products[i].Quantity} x {products[i].TotalPrice / products[i].Quantity} = {products[i].TotalPrice} so'm\n\n";
        }

        sendInfo += "Jami: " + products.Select(p => p.TotalPrice).Sum() + "so'm";

        await _botClient.SendTextMessageAsync(message.Chat.Id,
                                              sendInfo);
    }

    public async Task ControllBasket(Message message)
    {
        var roadWays = await _dbContext.RoadWays.Where(r => r.ChatId.Equals(message.Chat.Id)).ToListAsync();

        var products = await _dbContext.UserBaskets.Where(u => u.ChatId.Equals(message.Chat.Id)).ToListAsync();

        if(products.Count == 0 || products is null)
        {
            await ShowCategories(message);
            await _botClient.SendTextMessageAsync(message.Chat.Id,
                                             "Savat bo'sh");
            return;
        }

        if (roadWays.Count < 4)
        {
            var roadWay = new RoadWay()
            {
                Name = message.Text,
                ChatId = message.Chat.Id,
            };

            _dbContext.RoadWays.Add(roadWay);

            await ShowBasket(message);
        }
        else if(products.Select(c => ("❌ " + c.ProductName)).Contains(message.Text))
        {
            var product = products.FirstOrDefault(p=> ("❌ "+p.ProductName).Equals(message.Text));
            _dbContext.UserBaskets.Remove(product);
            await _dbContext.SaveChangesAsync();
            await ShowBasket(message);
        }
        else if(message.Text.Equals("🔄 Tozalash"))
        {
            foreach(var product in products)
            {
                _dbContext.UserBaskets.Remove(product);
            }

            _dbContext.RoadWays.Remove(roadWays[3]);
            await ShowCategories(message);
        }
        else if (message.Text.Equals("⬅️ Ortga"))
        {
            _dbContext.RoadWays.Remove(roadWays[3]);
            await ShowCategories(message);
        }
        else if(message.Text.Contains("🚘 Buyurtma berish"))
        {
            await _botClient.SendTextMessageAsync(message.Chat.Id,
                                                 "Buyurtma berildi");
            foreach (var product in products)
            {
                _dbContext.UserBaskets.Remove(product);
            }

            _dbContext.RoadWays.Remove(roadWays[3]);
            await ShowCategories(message);
        }

        await _dbContext.SaveChangesAsync();
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
