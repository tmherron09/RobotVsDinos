using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Weapon
    {
        public string name;
        public int attackPower;

        public Weapon(string name, Random rng)
        {
            this.name = name;
            InitializeAttackPower(rng);
        }

        public void InitializeAttackPower(Random rng)
        {
            attackPower = rng.Next(1, 6) * 10;  // returns 10 - 50 steps of 10
        }
    }
}
