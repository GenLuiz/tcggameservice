namespace spruedeckApi.Models;

public class GundamCardDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string GundamCardCollectionName { get; set; } = null!;
    public string GundamSetsCollectionName { get; set; } = null!;
}
