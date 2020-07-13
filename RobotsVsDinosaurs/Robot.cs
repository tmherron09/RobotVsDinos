using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Robot
    {
        string name;
        int health;
        int powerLevel; //stamina
        Weapon weapon;

        public Robot(string name, List<Weapon> weaponList, string weaponName)
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
    }
}
