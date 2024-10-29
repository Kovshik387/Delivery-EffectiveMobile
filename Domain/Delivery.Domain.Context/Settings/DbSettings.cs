namespace Delivery.Domain.Context.Settings;

public class DbSettings
{
    public const string SectionName  = "DbSettings";
    public string ConnectionString { get; set; } = string.Empty;
    public DbInitSettings? InitSettings { get; set; }
}

public class DbInitSettings
{
    public bool AddDemoData { get; set; }
}