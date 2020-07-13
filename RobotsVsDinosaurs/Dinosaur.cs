using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Dinosaur
    {
        string typename;
        int health;
        int powerLevel;
        int attackPower;
        //string attackName; Add Later

        public Dinosaur(string typeName)
        {
            this.typename = typeName;
            health = 100;
            powerLevel = 30; // stamina
            attackPower = InitializeAttackPower();
        }

        private int InitializeAttackPower()
        {
            Random rng = new Random();
            return rng.Next(1, 6) * 10; // returns an attack power between 10-50 at steps of 10
        }
    }
}
