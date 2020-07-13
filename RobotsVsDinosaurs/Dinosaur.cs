using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Dinosaur
    {
        public string typename;
        public int health;
        public int powerLevel;
        public int attackPower;
        Random rng;
        //string attackName; Add Later

        public Dinosaur(string typeName)
        {
            this.typename = typeName;
            health = 100;
            powerLevel = 30; // stamina
            rng = new Random();
            attackPower = InitializeAttackPower();
        }

        private int InitializeAttackPower()
        {
            int sleepTime = rng.Next(100, 201);
            Thread.Sleep(sleepTime);
            
            return rng.Next(1, 6) * 10; // returns an attack power between 10-50 at steps of 10
        }
    }
}
