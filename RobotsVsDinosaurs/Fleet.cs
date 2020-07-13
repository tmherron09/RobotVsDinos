using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Fleet
    {
        public List<Robot> robots;
        public List<Weapon> availableWeapons;

        public Fleet()
        {
            robots = new List<Robot>();
            availableWeapons = new List<Weapon>();
        }

        public void InitializeFleet(Random rng)
        {
            InitializeWeaponList(rng);
            InitializeNewRobotList();
        }
        private void InitializeNewRobotList()
        {
            string[] names = { "FrazelBot", "Storm 9000", "LED-Tron" };
            for (int i = 0; i < 3; i++)
            {
                robots.Add(new Robot(names[i], availableWeapons));
            }
        }
        private void InitializeWeaponList(Random rng)
        {
            List<string> weaponNames = new List<string> { "sword", "lazer sword", "blaster", "robot fists" };
            for (int i = 0; i < weaponNames.Count; i++)
            {
                string weaponName = weaponNames[i];
                availableWeapons.Add(new Weapon(weaponName, rng));
            }
        }


    }
}
