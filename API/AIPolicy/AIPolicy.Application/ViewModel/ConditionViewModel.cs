namespace AIPolicy.Application.ViewModel;

public class ConditionViewModel
{
    public int Id { get; set; }
    public int Type { get; set; }
    public string? Value { get; set; }
    public DateTime LastChange { get; set; }
    public ConditionViewModel? ConditionLeft { get; set; }
    public ConditionViewModel? ConditionRight { get; set; }
    public int SubNodeL { get; set; }
    public int SubNodeR { get; set; }
}