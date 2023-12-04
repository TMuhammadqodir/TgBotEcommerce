using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TelegramBotTask.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Tarkibi = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true),
                    TelNumber = table.Column<string>(type: "text", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreateAt", "Name", "UpdateAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8147), "Sho'volar", null },
                    { 2L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8149), "Choy", null },
                    { 3L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8150), "Kombo", null },
                    { 4L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8152), "Donorlar", null },
                    { 5L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8153), "Kotletlar", null },
                    { 6L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8154), "Kaboblar", null },
                    { 7L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8155), "Burgerlar", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreateAt", "Description", "Name", "Price", "Quantity", "Tarkibi", "UpdateAt" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8269), "Qovoqli qaymoqli sho'rva - Bu mazali taom bo'lib, sizni kuz fasli muhitiga chorlaydi . Bizning qovoqli sho'rva - bu noziklik va mazali ta'mni o'zida mujassam etgan haqiqiy pazandalik san'atidir. Quritilgan non bilan beriladi.", "Qovoqli sho'rva", 25000f, "450 ml", "piyoz, sabzi, nok, qovoq, tamat sousi, ziravorlar.", null },
                    { 2L, 1L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8272), "Bizning qo'ziqorin qaymoqli sho'rva - bu yumshoq qo'ziqorinlar, yangi sabzavotlar va qaymoqni oziga mujassam etadi. Quritilgan non bilan tortiladi.", "Qo'ziqorin kremli sho'rva", 40000f, "450 ml", "piyoz, qo'ziqorin, sut, qaymoq, ziravorlar.", null },
                    { 3L, 2L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8275), "Mevali choy - tetiklantiruvchi va xushbo'y ichimlik bo'lib, u o'zgacha narsalarni qidiradiganlar uchun juda yaxshi. Sharbatli meva ta'mi choy bilan birgalikda o'ziga xos ta'm tajribasini yaratadi.", "Choy mevali", 25000f, "350 m", "", null },
                    { 4L, 2L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8276), "Malinali choy - tazelik va shirinlikning ajoyib kombinatsiyasi. Yangi malinaning boy xushbo'yligi choyning nozikligi bilan birlashtirilib, hayratlanarli darajada ishtahani ochuvchi ichimlik yaratadi. Ushbu choyning har bir qultumi sizni malinaning yorqin ta'mi bilan to'ldiradi, ichimlikka o'ziga xos va tetiklantiruvchi xususiyat beradi.", "Malinali choy", 27000f, "350 ml", "", null },
                    { 5L, 3L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8278), "Rich and healthy, this set includes a delicious fish sandwich, a revitalizing arugula salad and your choice of an Americano or aromatic lemon tea. Enjoy the harmony of flavors and energy that this perfect combination brings.", "Kombo № 1", 70000f, "", "fish sandwich, salad with arugula 0.5 and a drink of your choice: Americano or tea with lemon.", null },
                    { 6L, 3L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8280), "Biz sizga 2-sonli kombinatni taqdim etamiz - ta'm va qulaylikni qadrlaydiganlar uchun ideal echim. Bu to‘plamga qishloq kartoshkalari, pishloq nuggetlari va muammosiz dam olish uchun Heinz pishloq sousi kiradi. Bunga engil va tetiklantiruvchi berry limonadini qo'shing va siz xohlagan joyda o'zingiz bilan olib ketishingiz mumkin bo'lgan to'liq zavqga ega bo'lasiz. Bizning Combo # 2 bilan xav", "Kombo № 1", 69000f, "", "qishloq kartoshkasi, pishloqli nuggetlar, Heinz pishloq sousi, berry limonadi 0,4 l.", null },
                    { 7L, 4L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8282), "Go'sht katta aylanadigan shishlarda pishiriladi, so'ngra u yupqa plastina shaklida kesiladi, bo'laklarga bo'linadi va ko'katlar va sabzavotlar bilan birga non ustiga qo'yiladi.", "Donar sendvich mol go'shti", 48000f, "350 gr", "bulochka, mol goʻshti, aysberg, pomidor, Iskander sousi, piyoz,xalapen.", null },
                    { 8L, 4L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8283), "Go'sht katta aylanadigan shishlarda pishiriladi, so'ngra u yupqa plastina shaklida kesiladi, bo'laklarga bo'linadi va ko'katlar va sabzavotlar bilan birga non ustiga qo'yiladi.\r\n", "Donar sendvich tovuq", 40000f, "350 gr", "bulochka, mol goʻshti, aysberg, pomidor, Iskander sousi, piyoz,xalapen.", null },
                    { 9L, 5L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8285), "Shirali tovuq bo'laklari, xushbo'y guruch va yangi tug'ralgan sabzavotlardan iborat boy va mazali taomdir. Ushbu taom tog'ri va muvozanatli ovqatlanishni qidiradiganlar uchun ideal tanlovdir. Turk yopilgan noni \"Ekmek\" bilan beriladi.", "Tovuq garnir bilan", 42000f, "350 gr", "aysberg, pomidor, bodring, guruch, tovuq filesi, Yekmek yassi non.", null },
                    { 10L, 6L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8287), "Bizning mol go'shtidan kabobimiz - og'izingizda ertib ketadigan bo'lib pishadi. Har bir mol go'shtining yuqori sifati va nozikligi sizning taomingizni unutilmas qiladi.\r\nBiz kaboblarimiz uchun eng barra va eng tabiiy masaliqlarni tanlashga alohida e'tibor beramiz. Bizning oshpazimiz sizga eng yaxshi oshpazlik tajribasini taqdim etish uchun faqat eng yaxshi go'sht bo'laklarini tanlaydi.", "Shashlik (mol go'shti)", 60000f, "380 gr", "mol go'shti, piyoz, mavsumiy sabzavotlar, guruch.", null },
                    { 11L, 6L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8289), "Bizning qoy go'shtidan tayyorlangan kabob barchaga haqiqiy ta'mni biluvchilar uchun  gastronomik taomdir. Sizga haqiqiy oshpazlik mahoratini taklif qilish uchun, biz faqat eng yumshoq va shirali qoy go'shtidan foydalanamiz.\r\nHar bir go‘sht bo‘lagi o‘zining tabiiy seli va o‘ziga xos ta’mini saqlab qolish uchun professional tarzda qayta ishlanadi. Biz qoy go'shtini yumshoq va aromatik tuzilishga ega b", "Shashlik (qoy go'shti)", 70000f, "380 gr", "qoy go'shti. piyoz, mavsumiy sabzavotlar, guruch.", null },
                    { 12L, 7L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8290), "Gamburger - bu xushbo'y ziravorlar, yangi bulochka va turli xil qo'shimchalar bilan kombinatsiyasi bo'lib, o'ziga xos ta'mni yaratadi.", "Gamburger", 23000f, "220 gr", "aysberg, bulochka, tuzlangan bodring, go'shtli kotlet, pomidor, mayonez.", null },
                    { 13L, 7L, new DateTime(2023, 12, 4, 8, 43, 25, 89, DateTimeKind.Utc).AddTicks(8292), "Gamburger - bu xushbo'y ziravorlar, yangi bulochka va turli xil qo'shimchalar bilan kombinatsiyasi bo'lib, o'ziga xos ta'mni yaratadi.", "Lavash", 24000f, "220 gr", "testtttttttttttttttttttttttttttttt", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
