namespace TelegramBotTask.Models;

public class UserInformation : Auditable
{
    public long ChatId { get; set; }
    public string? Name { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string TelNumber { get; set; }
}
