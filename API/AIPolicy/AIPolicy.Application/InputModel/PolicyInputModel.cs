using AIPolicy.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPolicy.Application.InputModel;

public class PolicyInputModel
{
    public int Version { get; set; }
    public List<Trigger> Triggers { get; set; }
    public DateTime LastChange { get; set; }
}
