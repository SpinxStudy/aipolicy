namespace AIPolicy.Core.Entity;

public class Trigger
{
    public static int MaxVersion { get; set; } = 0;

    public int Id { get; set; }
    public int IdPolicy { get; set; }
    public int Version { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Run { get; set; }
    public bool Active { get; set; }
    public bool AttackValid { get; set; }
    public Condition? RootCondition { get; set; }
    public DateTime LastChange { get; set; }
}
