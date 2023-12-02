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

    public DbSet<UserInformation> Users { get; set; }
}