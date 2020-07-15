using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    public class Warrior
    {
        public string name;
        public int health;
        public int powerLevel;
        public int maxPowerLevel;
        public int attackPower;

        public Warrior(string name, int health, int powerLevel, int maxPowerLevel, int attackPower)
        {
            this.name = name;
            this.health = health;
            this.powerLevel = powerLevel; // stamina
            this.maxPowerLevel = maxPowerLevel;
            this.attackPower = attackPower;
        }

        public int Attack(Warrior targetWarrior)
        {
            return targetWarrior.GetHit(attackPower);
        }
        public int GetHit(int attackPower)
        {
            health -= attackPower;
            return attackPower;
        }
        

    }
}
