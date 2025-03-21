using AIPolicy.Core.Entity;

namespace AIPolicy.Application.InputModel;

public class PolicyInputModel
{
    public int Version { get; set; }
    public List<Trigger> Triggers { get; set; }
    public DateTime LastChange { get; set; }
}
