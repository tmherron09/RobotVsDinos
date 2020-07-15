using System;
using System.Collections.Generic;

namespace RobotsVsDinosaurs
{
    class Herd : Army
    {
        public string warriorType; 
        public List<Warrior> dinosaurs;
        public List<string> dinosaurTypes;
        public bool isHuman;

        public Herd()
        {
            dinosaurs = new List<Warrior>();
            dinosaurTypes = new List<string> { "Troodon", "Quaesitosaurus", "T-Rex" };
            isHuman = false;
            warriorType = "Dinosaur";
        }

        #region Initialization Methods
        public void InitializeHerd(int health, int powerLevel, int maxPower, int attackPower)
        {
            InitializeNewDinosaurList(health, powerLevel, maxPower, attackPower);
        }
        private void InitializeNewDinosaurList(int health, int powerLevel, int maxPower, int attackPower)
        {
            for (int i = 0; i < 3; i++)
            {
                dinosaurs.Add(new Dinosaur(dinosaurTypes[i], health, powerLevel, maxPower, attackPower));
            }
        }
        #endregion

        #region Human Choice methods
        public Dinosaur HumanChooseDinosaurToFight(Battlefield battlefield)
        {
            string msg;

            int selection;
            bool valid = false;
            do
            {
                Console.WriteLine("Choose a dinosaur: ");
                for (int i = 0; i < dinosaurs.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {dinosaurs[i].name} | Power Level: {dinosaurs[i].powerLevel}");
                }
                valid = Int32.TryParse(Console.ReadLine(), out selection);
                if (valid)
                {
                    valid = selection > 0 && selection <= dinosaurs.Count;
                    selection--;
                }
                if (valid && dinosaurs[selection].powerLevel <= 0)
                {
                    valid = false;
                    msg = $"{dinosaurs[selection].name} has no power to fight.";
                    Console.WriteLine(msg);
                }
                if (!valid)
                {
                    battlefield.UpdateStatsDisplay();
                }
            } while (!valid);
            return dinosaurs[selection];
        }
        public Robot HumanChooseTargetRobot(Fleet fleet, Battlefield battlefield)
        {

            int selection;
            bool valid = false;
            do
            {
                Console.WriteLine("Choose a robot to attack: ");
                for (int i = 0; i < fleet.robots.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {fleet.robots[i].name} | Health: {fleet.robots[i].health}");
                }
                valid = Int32.TryParse(Console.ReadLine(), out selection);
                if (valid)
                {
                    valid = (selection > 0 && selection <= fleet.robots.Count);
                    selection--;
                }
                if (!valid)
                {
                    battlefield.UpdateStatsDisplay();
                }
            } while (!valid);
            return fleet.robots[selection];
        }
        #endregion

        #region Computer Choice Methods
        public Dinosaur ComputerChooseDinosaurToFight()
        {
            Dinosaur dinoWithMostHealth = dinosaurs[0];
            Dinosaur dinoWithMostPower = dinosaurs[0];
            int mostHealth = 0;
            int largestPowerLevel = 0;
            foreach (Dinosaur dino in dinosaurs)
            {
                if (dino.health >= mostHealth)
                {
                    mostHealth = dino.health;
                    dinoWithMostHealth = dino;
                }
                if (dino.powerLevel >= largestPowerLevel)
                {
                    largestPowerLevel = dino.powerLevel;
                    dinoWithMostPower = dino;
                }
            }
            if (dinoWithMostPower == null)
            {
                return dinoWithMostHealth;
            }
            if (dinoWithMostPower == dinoWithMostHealth)
            {
                return dinoWithMostPower;
            }
            else
            {
                return dinoWithMostPower;
            }
        }
        public Robot ComputerChooseTargetRobot(Fleet fleet, Random rng)
        {
            int leastHealth = 1000;
            int leastPower = 1000;
            // Set default to first in Dinosaur list to prevent uninitialized error
            Robot targetLeastHealthRobot = fleet.robots[0];
            Robot targetLeastPowerRobot = fleet.robots[0];
            foreach (Robot robot in fleet.robots)
            {
                if (robot.health < leastHealth)
                {
                    targetLeastHealthRobot = robot;
                }
                if (robot.powerLevel < leastPower)
                {
                    targetLeastPowerRobot = robot;
                }
            }
            // Computer will randomly target either the Dino with least health or Power, to help balance.
            if (rng.Next() % 2 == 0)
            {
                return targetLeastHealthRobot;
            }
            else
            {
                return targetLeastPowerRobot;
            }
        }
        #endregion

        #region Power Level Methods
        // Called to check if any dino in Dinosaurs List has energy left.
        public bool CheckIfDinosaursCanFight()
        {
            int hasPowerCount = 0;
            foreach (Dinosaur dino in dinosaurs)
            {
                if (dino.powerLevel > 0)
                {
                    hasPowerCount++;
                }
            }
            return hasPowerCount > 0 ? true : false;
        }
        // Updates power levels. Current dino decreases power, resting regain energy up to max
        public void UpdatePowerLevels(Dinosaur currentTurnDinosaur)
        {
            foreach (Dinosaur dino in dinosaurs)
            {
                if (currentTurnDinosaur == dino)
                {
                    currentTurnDinosaur.powerLevel -= 10;
                }
                else
                {
                    if (dino.powerLevel < dino.maxPowerLevel)
                    {
                        dino.powerLevel += 10;
                    }
                }
            }
        }
        // Updates power levels if all units are at 0 power. Add 10 power.
        public void UpdatePowerLevels()
        {
            foreach (Dinosaur dino in dinosaurs)
            {
                dino.powerLevel += 10;
            }
        }
        #endregion

        #region Uncategorized
        public bool CheckHasDied(Dinosaur dinosaur)
        {
            return dinosaur.health <= 0;
        }
        public void RemoveDinosaur(Dinosaur dinosaur)
        {
            if (dinosaurs.Contains(dinosaur))
            {
                dinosaurs.Remove(dinosaur);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
        #endregion
    }
}
