namespace _Project_.Contracts.Settings;

public class DatabaseSettings
{
    public const string SectionName = "DatabaseSettings";
    public string ConnectionString { get; set; } = default!;
}