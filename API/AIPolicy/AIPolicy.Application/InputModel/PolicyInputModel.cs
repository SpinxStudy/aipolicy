namespace AIPolicy.Application.InputModel;

public class PolicyInputModel
{
    public int Version { get; set; }
    public string? Name { get; set; }
    public List<TriggerInputModel> Triggers { get; set; } = new List<TriggerInputModel>();
}