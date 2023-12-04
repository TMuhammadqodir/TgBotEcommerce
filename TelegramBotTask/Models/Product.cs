namespace TelegramBotTask.Models;

public class Product : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Tarkibi { get; set; }
    public string? Quantity { get; set; }
    public float Price { get; set; }
    public long CategoryId  { get; set; }
}
