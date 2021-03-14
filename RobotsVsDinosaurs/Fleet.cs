using System;
using System.Collections.Generic;

namespace RobotsVsDinosaurs
{
    public class Fleet : Army
    {


        public List<Weapon> availableWeapons;


        public Fleet()
        {
            Warriors = new List<Warrior>();
            availableWeapons = new List<Weapon>();
            WarriorType = "Robot";
        }

        #region Fleet Initialization Methods
        // Calls the instantiation of a Weapon list and List of 3 new Robots.
        public void InitializeFleet(int health, int powerLevel, int maxPowerLevel)
        {
            InitializeWeaponList();
            InitializeNewRobotList(health, powerLevel, maxPowerLevel);
            if (!IsHuman)
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
                Warriors.Add(new Robot(availableWeapons, names[i], health, powerLevel, maxPowerLevel, 0));
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
            foreach (Robot robot in Warriors)
            {
                robot.ComputerChooseWeapon();
            }
        }
        #endregion

        #region Human Choice Methods
        // Display and read Human Player input of which Robot to use to fight.
        public Warrior HumanChooseRobotToFight(Battlefield battlefield)
        {
            string msg;
            int selection;
            bool valid = false;
            do
            {
                Console.WriteLine("Please select which robot to use: ");
                for (int i = 0; i < Warriors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) Name: {Warriors[i].Name} | Power Level: {Warriors[i].stamina}");
                }
                valid = Int32.TryParse(Console.ReadLine(), out selection);
                if (valid)
                {
                    valid = (selection > 0 && selection <= Warriors.Count);
                    selection--;
                    if (valid && Warriors[selection].stamina <= 0)
                    {
                        valid = false;
                        msg = $"{Warriors[selection].Name} has no power to fight.";
                        Console.WriteLine(msg);
                    }
                }
                if (!valid)
                {
                    battlefield.UpdateStatsDisplay();
                }
            } while (!valid);
            return Warriors[selection];
        }
        // Display and read Human Player input of which Dinosaur to attack.
        public Warrior HumanChooseTargetDinosaur(Army herd, Battlefield battlefield)
        {
            int selection;
            bool valid = false;
            do
            {
                Console.WriteLine("Choose a dinosaur to attack: ");
                for (int i = 0; i < herd.Warriors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {herd.Warriors[i].Name} | Health: {herd.Warriors[i].Health}");
                }
                valid = Int32.TryParse(Console.ReadLine(), out selection);
                if (valid)
                {
                    valid = (selection > 0 && selection <= herd.Warriors.Count);
                    selection--;
                }
                if (!valid)
                {
                    battlefield.UpdateStatsDisplay();
                }
            } while (!valid);
            return herd.Warriors[selection];
        }
        #endregion

        #region Computer Choice Methods
        // Logic for Computer to Choose a robot to fight with.
        public Warrior ComputerChooseRobotToFight(Random rng)
        {
            // If only one left skip logic check.
            if (Warriors.Count == 1)
            {
                return Warriors[0];
            }
            Warrior strongest = Warriors[0];
            Warrior robotWithMostPower = Warriors[0];
            int largestAttackPower = 0;
            int largestPowerLevel = 0;
            // Determin robots with most
            foreach (Robot robot in Warriors)
            {
                if (robot.weapon != null)
                {
                    if (robot.weapon.attackPower >= largestAttackPower)
                    {
                        largestAttackPower = robot.weapon.attackPower;
                        strongest = robot;
                    }
                }
                if (robot.stamina >= largestPowerLevel)
                {
                    largestPowerLevel = robot.stamina;
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
                return Warriors[rng.Next(0, Warriors.Count)];
            }
        }
        // Logic for Computer to choose target.
        public Warrior ComputeChooseTargetDinosaur(Herd herd, Random rng)
        {
            int leastHealth = 1000;
            int leastPower = 1000;
            // Set default to first in Dinosaur list to prevent uninitialized error
            Warrior targetLeastHealthDinosaur = herd.Warriors[0];
            Warrior targetLeastPowerDiinosaur = herd.Warriors[0];
            foreach (Dinosaur dino in herd.Warriors)
            {
                if (dino.Health < leastHealth)
                {
                    targetLeastHealthDinosaur = dino;
                }
                if (dino.stamina < leastPower)
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
            foreach (Robot robot in Warriors)
            {
                if (robot.stamina > 0)
                {
                    hasPowerCount++;
                }
            }
            return hasPowerCount > 0 ? true : false;
        }
        // Updates power levels. Current robot decreases power, resting regain energy up to max
        public void UpdatePowerLevels(Warrior currentTurnRobot)
        {
            foreach (Robot robot in Warriors)
            {
                if (currentTurnRobot == robot)
                {
                    currentTurnRobot.stamina -= 10;
                }
                else
                {
                    if (robot.stamina < robot.maxPowerLevel)
                    {
                        robot.stamina += 10;
                    }
                }
            }
        }
        // Updates power levels if all units are at 0 power. Add 10 power.
        public void UpdatePowerLevels()
        {
            // Called if no units have energy.
            foreach (Robot robot in Warriors)
            {
                robot.stamina += 10;
            }
        }
        #endregion

        #region Uncategorized
        // Returns true if a Robot has died- health <= 0
        public bool CheckHasDied(Warrior robot)
        {
            return robot.Health <= 0;
        }
        // Called if a robot has died and removes them from Robot List.
        public void RemoveRobot(Warrior robot)
        {
            if (Warriors.Contains(robot))
            {
                Warriors.Remove(robot);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
        #endregion

    }
}
