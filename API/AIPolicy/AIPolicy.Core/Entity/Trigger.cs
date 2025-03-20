using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPolicy.Core.Entity;

public class Trigger
{
    public static int MaxVersion { get; set; } = 0;

    public int Id { get; set; }
    public int Version { get; set; }
    public string Name { get; set; }
    public bool Run { get; set; }
    public bool Active { get; set; }
    public bool AttackValid { get; set; }
    public Condition RootCondition { get; set; }
    public DateTime LastChange { get; set; }

    public Trigger()
    {
        Id = 1;
        Version = 0;
        Name = "New trigger";
        Active = false;
        Run = false;
        AttackValid = false;
        RootCondition = new Condition();
    }
}
