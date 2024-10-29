namespace Delivery.Domain.Entities;

public class Log
{
    public int LogId { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}