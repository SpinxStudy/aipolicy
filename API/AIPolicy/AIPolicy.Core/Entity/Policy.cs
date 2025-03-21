namespace AIPolicy.Core.Entity;

public class Policy
{
    public static int MaxVersion { get; set; } = 0;

    public int Id { get; set; }
    public int Version { get; set; }
    public string? Name { get; set; }
    public DateTime LastChange { get; set; }
    public List<Trigger> Triggers { get; set; } = new List<Trigger>();
}
