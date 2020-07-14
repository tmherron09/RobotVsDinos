using System;
using System.Collections.Generic;

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

        #region Fleet Initialization Methods
        // Calls the instantiation of a Weapon list and List of 3 new Robots.
        public void InitializeFleet(int health, int powerLevel, int maxPowerLevel)
        {
            InitializeWeaponList();
            InitializeNewRobotList(health, powerLevel, maxPowerLevel);
            if (!isHuman)
            {
                ComputerChooseAllWeapons();
            }
        }
        // Instantiates three new Robots based on hard coded names into the Robot List.
        private void InitializeNewRobotList(int health, int powerLevel, int maxPowerLevel)
        {
            string[] names = { "FrazelBot", "Storm 9000", "LED-Tron" };
            for (int i = 0; i < 3; i++)
            {
                robots.Add(new Robot(names[i], availableWeapons, health, powerLevel, maxPowerLevel));
            }
        }
        // Instantiates a WeaponList based on hard coded values.
        private void InitializeWeaponList()
        {
            List<string> weaponNames = new List<string> { "sword", "lazer sword", "blaster", "robot fists", "Rocket Launcher", "Wrecking Ball", "Flamethrower", "Magnified Loud Speaker" };
            List<int> weaponAttackPowers = new List<int> { 30, 50, 20, 30, 20, 10, 30, 40 };
            for (int i = 0; i < weaponNames.Count; i++)
            {
                availableWeapons.Add(new Weapon(weaponNames[i], weaponAttackPowers[i]));
            }
        }
        public void ComputerChooseAllWeapons()
        {
            foreach (Robot robot in robots)
            {
                robot.ComputerChooseWeapon();
            }
        }
        #endregion

        #region Human Choice Methods
        // Display and read Human Player input of which Robot to use to fight.
        public Robot HumanChooseRobotToFight(Battlefield battlefield)
        {
            string msg;
            int selection;
            bool valid = false;
            do
            {
                Console.WriteLine("Please select which robot to use: ");
                for (int i = 0; i < robots.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) Name: {robots[i].name} | Power Level: {robots[i].powerLevel}");
                }
                valid = Int32.TryParse(Console.ReadLine(), out selection);
                if (valid)
                {
                    valid = (selection > 0 && selection <= robots.Count);
                    selection--;
                    if (valid && robots[selection].powerLevel <= 0)
                    {
                        valid = false;
                        msg = $"{robots[selection].name} has no power to fight.";
                        Console.WriteLine(msg);
                    }
                }
                if (!valid)
                {
                    battlefield.UpdateStatsDisplay();
                }
            } while (!valid);
            return robots[selection];
        }
        // Display and read Human Player input of which Dinosaur to attack.
        public Dinosaur HumanChooseTargetDinosaur(Herd herd, Battlefield battlefield)
        {
            int selection;
            bool valid = false;
            do
            {
                Console.WriteLine("Choose a dinosaur to attack: ");
                for (int i = 0; i < herd.dinosaurs.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {herd.dinosaurs[i].typename} | Health: {herd.dinosaurs[i].health}");
                }
                valid = Int32.TryParse(Console.ReadLine(), out selection);
                if (valid)
                {
                    valid = (selection > 0 && selection <= herd.dinosaurs.Count);
                    selection--;
                }
                if (!valid)
                {
                    battlefield.UpdateStatsDisplay();
                }
            } while (!valid);
            return herd.dinosaurs[selection];
        }
        #endregion

        #region Computer Choice Methods
        // Logic for Computer to Choose a robot to fight with.
        public Robot ComputerChooseRobotToFight(Random rng)
        {
            // If only one left skip logic check.
            if (robots.Count == 1)
            {
                return robots[0];
            }
            Robot strongest = robots[0];
            Robot robotWithMostPower = robots[0];
            int largestAttackPower = 0;
            int largestPowerLevel = 0;
            // Determin robots with most
            foreach (Robot robot in robots)
            {
                if (robot.weapon != null)
                {
                    if (robot.weapon.attackPower >= largestAttackPower)
                    {
                        largestAttackPower = robot.weapon.attackPower;
                        strongest = robot;
                    }
                }
                if (robot.powerLevel >= largestPowerLevel)
                {
                    largestPowerLevel = robot.powerLevel;
                    robotWithMostPower = robot;
                }
            }
            // Branch logic with rng to create prevent patterns.
            if (robotWithMostPower == strongest)
            {
                return strongest;
            }
            else if (rng.Next(0, 101) < 20)
            {
                return robotWithMostPower;
            }
            else if (rng.Next(0, 101) < 65)
            {
                return strongest;
            }
            else
            {
                return robots[rng.Next(0, robots.Count)];
            }
        }
        // Logic for Computer to choose target.
        public Dinosaur ComputeChooseTargetDinosaur(Herd herd, Random rng)
        {
            int leastHealth = 1000;
            int leastPower = 1000;
            // Set default to first in Dinosaur list to prevent uninitialized error
            Dinosaur targetLeastHealthDinosaur = herd.dinosaurs[0];
            Dinosaur targetLeastPowerDiinosaur = herd.dinosaurs[0];
            foreach (Dinosaur dino in herd.dinosaurs)
            {
                if (dino.health < leastHealth)
                {
                    targetLeastHealthDinosaur = dino;
                }
                if (dino.powerLevel < leastPower)
                {
                    targetLeastPowerDiinosaur = dino;
                }
            }
            // Computer will randomly target either the Dino with least health or Power, to help balance.
            if (rng.Next() % 2 == 0)
            {
                return targetLeastHealthDinosaur;
            }
            else
            {
                return targetLeastPowerDiinosaur;
            }
        }
        #endregion

        #region Power Level Methods
        // Called to check if any robot in Robots List has energy left.
        public bool CheckIfRobotsCanFight()
        {
            int hasPowerCount = 0;
            foreach (Robot robot in robots)
            {
                if (robot.powerLevel > 0)
                {
                    hasPowerCount++;
                }
            }
            return hasPowerCount > 0 ? true : false;
        }
        // Updates power levels. Current robot decreases power, resting regain energy up to max
        public void UpdatePowerLevels(Robot currentTurnRobot)
        {
            foreach (Robot robot in robots)
            {
                if (currentTurnRobot == robot)
                {
                    currentTurnRobot.powerLevel -= 10;
                }
                else
                {
                    if (robot.powerLevel < robot.maxPowerLevel)
                    {
                        robot.powerLevel += 10;
                    }
                }
            }
        }
        // Updates power levels if all units are at 0 power. Add 10 power.
        public void UpdatePowerLevels()
        {
            // Called if no units have energy.
            foreach (Robot robot in robots)
            {
                robot.powerLevel += 10;
            }
        }
        #endregion

        #region Uncategorized
        // Returns true if a Robot has died- health <= 0
        public bool CheckHasDied(Robot robot)
        {
            return robot.health <= 0;
        }
        // Called if a robot has died and removes them from Robot List.
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
        #endregion

    }
}
