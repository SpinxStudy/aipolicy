using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPolicy.Core.Entity;

public class Condition
{
    public int Id { get; set; }
    public string Value { get; set; }
    public int Type { get; set; }
    public Condition ConditionLeft { get; set; }
    public int SubNodeL { get; set; }
    public Condition ConditionRight { get; set; }
    public int SubNodeR { get; set; }
    public DateTime LastChange { get; set; }

    public Condition()
    {
        Id = 2;
        Value = "";
        Type = 3;
    }
}
