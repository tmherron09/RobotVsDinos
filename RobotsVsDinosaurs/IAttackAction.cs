using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    public interface IAttackAction
    {
        string AttackName { get; set; }
        string AttackVerb { get; set; }
        int AttackPower { get; set; }
        
        


    }
}
