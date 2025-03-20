using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPolicy.Core.Entity;

public class Trigger
{
    public int Id { get; set; }
    public int Version { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; }
    public bool AttackValid { get; set; }
    public string? RootConditions { get; set; }
    public string? Operations { get; set; }
}
