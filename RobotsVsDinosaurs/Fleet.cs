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
        public bool isHuman;

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
            List<string> weaponNames = new List<string> { "sword", "lazer sword", "blaster", "robot fists", "Rocket Launcher", "Wrecking Ball", "Flamethrower", "Magnified Loud Speaker" };
            List<int> weaponAttackPowers = new List<int> { 30, 50, 20, 30, 20, 10, 30, 40 };
            for (int i = 0; i < weaponNames.Count; i++)
            {
                availableWeapons.Add(new Weapon(weaponNames[i], weaponAttackPowers[i]));
            }
        }
        public Robot ChooseRobotToFight()
        {
           
                Console.WriteLine("Please select which robot to use: ");
                for(int i = 0; i < robots.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {robots[i].name}");
                }
                int selection;
                bool valid = false;
                do
                {
                    valid = Int32.TryParse(Console.ReadLine(), out selection);
                    if (valid)
                    {
                        valid = (selection > 0 && selection <= robots.Count);
                        selection--;
                    }
                } while (!valid);
                return robots[selection];
        }
        public Robot ComputerChooseRobotToFight()
        {
            Robot strongest = robots[0];
            Robot robotWithMostPower = robots[0];
            int largestAttackPower = 0;
            int largestPowerLevel = 0;
            foreach (Robot robot in robots)
            {
                foreach (Weapon weapon in robot.weaponList)
                {
                    if (weapon.attackPower >= largestAttackPower)
                    {
                        largestAttackPower = weapon.attackPower;
                        strongest = robot;
                    }
                }
                if (robot.powerLevel >= largestPowerLevel)
                {
                    largestPowerLevel = robot.powerLevel;
                    robotWithMostPower = robot;
                }
            }
            if (robotWithMostPower == null)
            {
                return strongest;
            }
            if (robotWithMostPower == strongest)
            {
                return robotWithMostPower;
            }
            else
            {
                return robotWithMostPower;
            }
        }
        public bool CheckHasDied(Robot robot)
        {
            return robot.health <= 0;
        }
        public void RemoveRobot(Robot robot)
        {
            if (robots.Contains(robot))
            {
                robots.Remove(robot);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
        public Dinosaur ComputeChooseTargetDinosaur(Herd herd, Random rng)
        {
            int leastHealth = 1000;
            int leastPower = 1000;
            // Set default to first in Dinosaur list to prevent uninitialized error
            Dinosaur targetLeastHealthDinosaur = herd.dinosaurs[0];
            Dinosaur targetLeastPowerDiinosaur = herd.dinosaurs[0];
            foreach(Dinosaur dino in herd.dinosaurs)
            {
                if(dino.health < leastHealth)
                {
                    targetLeastHealthDinosaur = dino;
                }
                if(dino.powerLevel < leastPower)
                {
                    targetLeastPowerDiinosaur = dino;
                }
            }
            // Computer will randomly target either the Dino with least health or Power, to help balance.
            if(rng.Next() % 2 == 0)
            {
                return targetLeastHealthDinosaur;
            }
            else
            {
                return targetLeastPowerDiinosaur;
            }
        }

        public Dinosaur HumanChooseTargetDinosaur(Herd herd)
        {
            Console.WriteLine("Choose a dinosaur to attack: ");
            for (int i = 0; i < herd.dinosaurs.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {herd.dinosaurs[i].typename} Attack Power: {herd.dinosaurs[i].attackPower}");
            }
            int selection;
            bool valid = false;
            do
            {
                valid = Int32.TryParse(Console.ReadLine(), out selection);
                if (valid)
                {
                    valid = (selection > 0 && selection <= herd.dinosaurs.Count);
                    selection--;
                }
            } while (!valid);
            return herd.dinosaurs[selection];
        }
    }
}
