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
            //WarriorType = "Robot";
        }

        #region Fleet Initialization Methods
        // Calls the instantiation of a Weapon list and List of 3 new Robots.
        // TODO: Call Base Attack Power Somwhere
        public override void InitializeArmy(float health, float powerLevel, float maxPowerLevel, float baseAttackPower)
        {
            InitializeWeaponList();
            InitializeNewRobotList(health, powerLevel, maxPowerLevel);
            if (!IsHuman)
            {
                ComputerChooseAllWeapons();
            }
        }
        // Instantiates three new Robots based on hard coded names into the Robot List.
        private void InitializeNewRobotList(float health, float powerLevel, float maxPowerLevel)
        {
            string[] names = { "FrazelBot", "Storm 9000", "LED-Tron" };
            for (int i = 0; i < 3; i++)
            {
                Warriors.Add(new Robot(this, ref availableWeapons, names[i], health, powerLevel, maxPowerLevel, 0));
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
            float largestAttackPower = 0;
            float largestPowerLevel = 0;
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
                if (robot.Stamina >= largestPowerLevel)
                {
                    largestPowerLevel = robot.Stamina;
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
                if (dino.Stamina < leastPower)
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
                if (robot.Stamina > 0)
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
                    currentTurnRobot.Stamina -= 10;
                }
                else
                {
                    if (robot.Stamina < robot.maxPowerLevel)
                    {
                        robot.Stamina += 10;
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
                robot.Stamina += 10;
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
