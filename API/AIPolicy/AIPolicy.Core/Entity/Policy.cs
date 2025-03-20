using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPolicy.Core.Entity;

public class Policy
{
    public static int MaxVersion { get; set; } = 0;

    public int Id { get; set; }
    public int Version { get; set; }
    public List<Trigger> Triggers { get; set; }

    public Policy()
    {
        Version = 0;
        Id = 0;
        Triggers = new List<Trigger>();
    }
}
