using System;
using System.Collections.Generic;

namespace RobotsVsDinosaurs
{
    public class Herd : Army
    {
        public List<string> dinosaurTypes;

        public Herd()
        {
            Warriors = new List<Warrior>();
            dinosaurTypes = new List<string> { "Troodon", "Quaesitosaurus", "T-Rex" };
            IsHuman = false;
            WarriorType = "Dinosaur";
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
                Warriors.Add(new Dinosaur(dinosaurTypes[i], health, powerLevel, maxPower, attackPower));
            }
        }
        #endregion

        #region Human Choice methods
        public Warrior HumanChooseDinosaurToFight(Battlefield battlefield)
        {
            string msg;

            int selection;
            bool valid = false;
            do
            {
                Console.WriteLine("Choose a dinosaur: ");
                for (int i = 0; i < Warriors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {Warriors[i].Name} | Power Level: {Warriors[i].stamina}");
                }
                valid = Int32.TryParse(Console.ReadLine(), out selection);
                if (valid)
                {
                    valid = selection > 0 && selection <= Warriors.Count;
                    selection--;
                }
                if (valid && Warriors[selection].stamina <= 0)
                {
                    valid = false;
                    msg = $"{Warriors[selection].Name} has no power to fight.";
                    Console.WriteLine(msg);
                }
                if (!valid)
                {
                    battlefield.UpdateStatsDisplay();
                }
            } while (!valid);
            return Warriors[selection];
        }
        public Warrior HumanChooseTargetRobot(Fleet fleet, Battlefield battlefield)
        {

            int selection;
            bool valid = false;
            do
            {
                Console.WriteLine("Choose a robot to attack: ");
                for (int i = 0; i < fleet.Warriors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {fleet.Warriors[i].Name} | Health: {fleet.Warriors[i].Health}");
                }
                valid = Int32.TryParse(Console.ReadLine(), out selection);
                if (valid)
                {
                    valid = (selection > 0 && selection <= fleet.Warriors.Count);
                    selection--;
                }
                if (!valid)
                {
                    battlefield.UpdateStatsDisplay();
                }
            } while (!valid);
            return fleet.Warriors[selection];
        }
        #endregion

        #region Computer Choice Methods
        public Warrior ComputerChooseDinosaurToFight()
        {
            Warrior dinoWithMostHealth = Warriors[0];
            Warrior dinoWithMostPower = Warriors[0];
            float mostHealth = 0;
            int largestPowerLevel = 0;
            foreach (Dinosaur dino in Warriors)
            {
                if (dino.Health >= mostHealth)
                {
                    mostHealth = dino.Health;
                    dinoWithMostHealth = dino;
                }
                if (dino.stamina >= largestPowerLevel)
                {
                    largestPowerLevel = dino.stamina;
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
        public Warrior ComputerChooseTargetRobot(Fleet fleet, Random rng)
        {
            int leastHealth = 1000;
            int leastPower = 1000;
            // Set default to first in Dinosaur list to prevent uninitialized error
            Warrior targetLeastHealthRobot = fleet.Warriors[0];
            Warrior targetLeastPowerRobot = fleet.Warriors[0];
            foreach (Robot robot in fleet.Warriors)
            {
                if (robot.Health < leastHealth)
                {
                    targetLeastHealthRobot = robot;
                }
                if (robot.stamina < leastPower)
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
            foreach (Dinosaur dino in Warriors)
            {
                if (dino.stamina > 0)
                {
                    hasPowerCount++;
                }
            }
            return hasPowerCount > 0 ? true : false;
        }
        // Updates power levels. Current dino decreases power, resting regain energy up to max
        public void UpdatePowerLevels(Warrior currentTurnDinosaur)
        {
            foreach (Dinosaur dino in Warriors)
            {
                if (currentTurnDinosaur == dino)
                {
                    currentTurnDinosaur.stamina -= 10;
                }
                else
                {
                    if (dino.stamina < dino.maxPowerLevel)
                    {
                        dino.stamina += 10;
                    }
                }
            }
        }
        // Updates power levels if all units are at 0 power. Add 10 power.
        public void UpdatePowerLevels()
        {
            foreach (Dinosaur dino in Warriors)
            {
                dino.stamina += 10;
            }
        }
        #endregion

        #region Uncategorized
        public bool CheckHasDied(Warrior dinosaur)
        {
            return dinosaur.Health <= 0;
        }
        public void RemoveDinosaur(Warrior dinosaur)
        {
            if (Warriors.Contains(dinosaur))
            {
                Warriors.Remove(dinosaur);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
        #endregion
    }
}
