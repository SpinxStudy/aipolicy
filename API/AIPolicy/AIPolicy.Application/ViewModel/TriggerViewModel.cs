namespace AIPolicy.Application.ViewModel;

public class TriggerViewModel
{
    public int Id { get; set; }
    public int Version { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; }
    public bool Run { get; set; }
    public bool AttackValid { get; set; }
    public DateTime LastChange { get; set; }
    public ConditionViewModel? RootCondition { get; set; }
}