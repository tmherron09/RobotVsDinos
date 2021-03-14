using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    public abstract class Army
    {
        public string WarriorType { get; set; }
        public List<Warrior> Warriors { get; set; }
        public bool IsHuman { get; set; }


        public bool CheckIfWarriorsCanFight()
        {
            int hasPowerCount = 0;
            foreach (Warrior warrior in Warriors)
            {
                if (warrior.stamina > 0)
                {
                    hasPowerCount++;
                }
            }
            return hasPowerCount > 0 ? true : false;
        }

        public Warrior HumanChooseWarriorToFight(Battlefield battlefield, Army currentArmy)
        {
            throw new NotImplementedException();
        }
        

        public virtual Warrior HumanChooseRobotToFight(Battlefield battlefield)
        {
            throw new NotImplementedException();
        }
    }
}
