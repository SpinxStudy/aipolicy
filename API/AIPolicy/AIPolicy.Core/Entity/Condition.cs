namespace AIPolicy.Core.Entity;

public class Condition
{
    public int Id { get; set; }
    public int IdTrigger { get; set; }
    public int Type { get; set; }
    public string? Value { get; set; }
    public Condition? ConditionLeft { get; set; }
    public int ConditionLeftId { get; set; }
    public int SubNodeL { get; set; }
    public Condition? ConditionRight { get; set; }
    public int ConditionRightId { get; set; }
    public int SubNodeR { get; set; }
    public DateTime LastChange { get; set; }
}
