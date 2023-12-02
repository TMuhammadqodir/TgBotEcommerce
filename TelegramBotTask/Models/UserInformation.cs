namespace TelegramBotTask.Models;

public class UserInformation
{
    public long Id { get; set; }
    public long ChatId { get; set; }
    public string? Name { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string TelNumber { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateAt { get; set; }
}
