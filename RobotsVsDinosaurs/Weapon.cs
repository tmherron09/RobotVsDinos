using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Weapon
    {
        string name;
        int attackPower;

        public Weapon(string name)
        {
            this.name = name;
            Random rng = new Random();
            attackPower = rng.Next(1, 6) * 10;  // returns 10 - 50 steps of 10
        }
    }
}
