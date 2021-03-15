using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    public abstract class Army
    {
        //public string WarriorType { get; set; }
        public List<Warrior> Warriors { get; set; }
        public bool IsHuman { get; set; }

        public abstract void InitializeArmy(float health, float powerLevel, float maxPowerLevel, float baseAttackPower);

        public bool CheckIfWarriorsCanFight()
        {
            int hasPowerCount = 0;
            foreach (Warrior warrior in Warriors)
            {
                if (warrior.Stamina > 0)
                {
                    hasPowerCount++;
                }
            }
            return hasPowerCount > 0 ? true : false;
        }

        public string GetArmyWarriorType()
        {
            switch (this)
            {
                case Fleet f:
                    return "Robot";
                case Herd h:
                    return "Dinosaur";
                default:
                    throw new Exception("Invalid Army type detected.");
            }
        }

        public Warrior HumanChooseWarriorToFight(Battlefield battlefield, Army currentArmy)
        {
            throw new NotImplementedException();
        }
        


    }
}
