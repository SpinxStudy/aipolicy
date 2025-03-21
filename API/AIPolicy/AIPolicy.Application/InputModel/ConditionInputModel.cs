namespace AIPolicy.Application.InputModel;

public class ConditionInputModel
{
    public int Type { get; set; }
    public string? Value { get; set; }
    public ConditionInputModel? ConditionLeft { get; set; }
    public ConditionInputModel? ConditionRight { get; set; }
    public int SubNodeL { get; set; }
    public int SubNodeR { get; set; }
}
