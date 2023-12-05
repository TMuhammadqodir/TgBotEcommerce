using Microsoft.EntityFrameworkCore;
using TelegramBotTask.Models;

namespace TelegramBotTask.DbContexts;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql(DatabasePath.ConnectionString);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<RoadWay> RoadWays { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<UserInformation> Users { get; set; }
    public DbSet<UserBasket> UserBaskets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Seed Data
        modelBuilder.Entity<Category>().HasData(
             new Category { Id = 1 , Name = "Sho'volar", CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Category { Id = 2 , Name = "Choy", CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Category { Id = 3 , Name = "Kombo", CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Category { Id = 4 , Name = "Donorlar", CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Category { Id = 5 , Name = "Kotletlar", CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Category { Id = 6 , Name = "Kaboblar", CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Category { Id = 7 , Name = "Burgerlar", CreateAt = DateTime.UtcNow, UpdateAt = null }


             );

        modelBuilder.Entity<Product>().HasData(
             new Product { Id = 1, Name = "Qovoqli sho'rva", CategoryId = 1, Description= "Qovoqli qaymoqli sho'rva - Bu mazali taom bo'lib, sizni kuz fasli muhitiga chorlaydi . Bizning qovoqli sho'rva - bu noziklik va mazali ta'mni o'zida mujassam etgan haqiqiy pazandalik san'atidir. Quritilgan non bilan beriladi.", Tarkibi= "piyoz, sabzi, nok, qovoq, tamat sousi, ziravorlar.", Quantity = "450 ml", Price = 25000, CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Product { Id = 2, Name = "Qo'ziqorin kremli sho'rva", CategoryId = 1, Description = "Bizning qo'ziqorin qaymoqli sho'rva - bu yumshoq qo'ziqorinlar, yangi sabzavotlar va qaymoqni oziga mujassam etadi. Quritilgan non bilan tortiladi.", Tarkibi= "piyoz, qo'ziqorin, sut, qaymoq, ziravorlar.", Quantity= "450 ml", Price = 40000,  CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Product { Id = 3, Name = "Choy mevali", CategoryId = 2, Description = "Mevali choy - tetiklantiruvchi va xushbo'y ichimlik bo'lib, u o'zgacha narsalarni qidiradiganlar uchun juda yaxshi. Sharbatli meva ta'mi choy bilan birgalikda o'ziga xos ta'm tajribasini yaratadi.", Tarkibi="", Quantity= "350 m", Price = 25000,  CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Product { Id = 4, Name = "Malinali choy", CategoryId =2, Description = "Malinali choy - tazelik va shirinlikning ajoyib kombinatsiyasi. Yangi malinaning boy xushbo'yligi choyning nozikligi bilan birlashtirilib, hayratlanarli darajada ishtahani ochuvchi ichimlik yaratadi. Ushbu choyning har bir qultumi sizni malinaning yorqin ta'mi bilan to'ldiradi, ichimlikka o'ziga xos va tetiklantiruvchi xususiyat beradi.", Tarkibi="", Quantity="350 ml", Price = 27000,  CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Product { Id = 5, Name = "Kombo № 1", CategoryId = 3, Description = "Rich and healthy, this set includes a delicious fish sandwich, a revitalizing arugula salad and your choice of an Americano or aromatic lemon tea. Enjoy the harmony of flavors and energy that this perfect combination brings.", Tarkibi= "fish sandwich, salad with arugula 0.5 and a drink of your choice: Americano or tea with lemon.", Quantity="", Price = 70000,  CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Product { Id = 6, Name = "Kombo № 1", CategoryId = 3, Description = "Biz sizga 2-sonli kombinatni taqdim etamiz - ta'm va qulaylikni qadrlaydiganlar uchun ideal echim. Bu to‘plamga qishloq kartoshkalari, pishloq nuggetlari va muammosiz dam olish uchun Heinz pishloq sousi kiradi. Bunga engil va tetiklantiruvchi berry limonadini qo'shing va siz xohlagan joyda o'zingiz bilan olib ketishingiz mumkin bo'lgan to'liq zavqga ega bo'lasiz. Bizning Combo # 2 bilan xav", Tarkibi= "qishloq kartoshkasi, pishloqli nuggetlar, Heinz pishloq sousi, berry limonadi 0,4 l.", Quantity="", Price = 69000,  CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Product { Id = 7, Name = "Donar sendvich mol go'shti", CategoryId = 4, Description = "Go'sht katta aylanadigan shishlarda pishiriladi, so'ngra u yupqa plastina shaklida kesiladi, bo'laklarga bo'linadi va ko'katlar va sabzavotlar bilan birga non ustiga qo'yiladi.", Tarkibi= "bulochka, mol goʻshti, aysberg, pomidor, Iskander sousi, piyoz,xalapen.", Quantity="350 gr", Price = 48000,  CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Product { Id = 8, Name = "Donar sendvich tovuq", CategoryId = 4, Description = "Go'sht katta aylanadigan shishlarda pishiriladi, so'ngra u yupqa plastina shaklida kesiladi, bo'laklarga bo'linadi va ko'katlar va sabzavotlar bilan birga non ustiga qo'yiladi.\r\n", Tarkibi= "bulochka, mol goʻshti, aysberg, pomidor, Iskander sousi, piyoz,xalapen.", Quantity="350 gr", Price = 40000,  CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Product { Id = 9, Name = "Tovuq garnir bilan", CategoryId = 5, Description = "Shirali tovuq bo'laklari, xushbo'y guruch va yangi tug'ralgan sabzavotlardan iborat boy va mazali taomdir. Ushbu taom tog'ri va muvozanatli ovqatlanishni qidiradiganlar uchun ideal tanlovdir. Turk yopilgan noni \"Ekmek\" bilan beriladi.", Tarkibi= "aysberg, pomidor, bodring, guruch, tovuq filesi, Yekmek yassi non.", Quantity="350 gr", Price = 42000,  CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Product { Id = 10, Name = "Shashlik (mol go'shti)", CategoryId = 6, Description = "Bizning mol go'shtidan kabobimiz - og'izingizda ertib ketadigan bo'lib pishadi. Har bir mol go'shtining yuqori sifati va nozikligi sizning taomingizni unutilmas qiladi.\r\nBiz kaboblarimiz uchun eng barra va eng tabiiy masaliqlarni tanlashga alohida e'tibor beramiz. Bizning oshpazimiz sizga eng yaxshi oshpazlik tajribasini taqdim etish uchun faqat eng yaxshi go'sht bo'laklarini tanlaydi.", Tarkibi= "mol go'shti, piyoz, mavsumiy sabzavotlar, guruch.", Quantity="380 gr", Price = 60000,  CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Product { Id = 11, Name = "Shashlik (qoy go'shti)", CategoryId = 6, Description = "Bizning qoy go'shtidan tayyorlangan kabob barchaga haqiqiy ta'mni biluvchilar uchun  gastronomik taomdir. Sizga haqiqiy oshpazlik mahoratini taklif qilish uchun, biz faqat eng yumshoq va shirali qoy go'shtidan foydalanamiz.\r\nHar bir go‘sht bo‘lagi o‘zining tabiiy seli va o‘ziga xos ta’mini saqlab qolish uchun professional tarzda qayta ishlanadi. Biz qoy go'shtini yumshoq va aromatik tuzilishga ega b", Tarkibi= "qoy go'shti. piyoz, mavsumiy sabzavotlar, guruch.", Quantity="380 gr", Price = 70000,  CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Product { Id = 12, Name = "Gamburger", CategoryId = 7,  Description = "Gamburger - bu xushbo'y ziravorlar, yangi bulochka va turli xil qo'shimchalar bilan kombinatsiyasi bo'lib, o'ziga xos ta'mni yaratadi.", Tarkibi= "aysberg, bulochka, tuzlangan bodring, go'shtli kotlet, pomidor, mayonez.", Quantity="220 gr", Price = 23000,  CreateAt = DateTime.UtcNow, UpdateAt = null },
             new Product { Id = 13, Name = "Lavash", CategoryId =7, Description = "Gamburger - bu xushbo'y ziravorlar, yangi bulochka va turli xil qo'shimchalar bilan kombinatsiyasi bo'lib, o'ziga xos ta'mni yaratadi.", Tarkibi="testtttttttttttttttttttttttttttttt", Quantity="220 gr", Price = 24000,  CreateAt = DateTime.UtcNow, UpdateAt = null }
             );
        #endregion
    }
}