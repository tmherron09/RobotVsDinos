using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Army
    {
        public string warriorType { get; set; }
        public List<Warrior> warriors;
        public bool isHuman;
        public bool CheckIfWarriorsCanFight()
        {
            int hasPowerCount = 0;
            foreach (Warrior warrior in warriors)
            {
                if (warrior.powerLevel > 0)
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
