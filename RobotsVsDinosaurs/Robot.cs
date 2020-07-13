using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Robot
    {
        public string name;
        public int health;
        public int powerLevel; //stamina
        public Weapon weapon;

        public Robot(string name, List<Weapon> weaponList)
        {
            this.name = name;
            this.weapon = InitializeWeapon(weaponList);
            health = 100;
            powerLevel = 30;
        }

        private Weapon InitializeWeapon(List<Weapon> weaponList)
        {
            return weaponList[0];
        }

        public void Attack(Dinosaur targetDinosaur)
        {
            targetDinosaur.GetHit(this.weapon);
        }
        public void GetHit(int attackPower)
        {
            health -= attackPower;
        }
    }
}
