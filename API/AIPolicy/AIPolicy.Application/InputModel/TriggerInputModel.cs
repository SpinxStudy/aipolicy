namespace AIPolicy.Application.InputModel;

public class TriggerInputModel
{
    public int Version { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; }
    public bool Run { get; set; }
    public bool AttackValid { get; set; }
    public ConditionInputModel? RootCondition { get; set; }
}
