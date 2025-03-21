namespace AIPolicy.Application.ViewModel;

public class PolicyViewModel
{
    public int Id { get; set; }
    public int Version { get; set; }
    public string? Name { get; set; }
    public DateTime LastChange { get; set; }
    public List<TriggerViewModel> Triggers { get; set; } = new List<TriggerViewModel>();
}