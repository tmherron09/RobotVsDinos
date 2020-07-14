using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Herd
    {
        public List<Dinosaur> dinosaurs;
        public List<string> dinosaurTypes;
        public bool isHuman;
        //List<DinosaurAttack> dinosaurAttacks;

        public Herd()
        {
            dinosaurs = new List<Dinosaur>();
            dinosaurTypes = new List<string> { "Troodon", "Quaesitosaurus", "T-Rex" };
            isHuman = false;
        }

        public void InitializeHerd()
        {
            InitializeNewDinosaurList();
        }
        private void InitializeNewDinosaurList()
        {
            for (int i = 0; i < 3; i++)
            {
                dinosaurs.Add(new Dinosaur(dinosaurTypes[i]));
            }
        }
        public Dinosaur ChooseDinosaurToFight()
        {
                Console.WriteLine("Choose a dinosaur: ");
                for (int i = 0; i < dinosaurs.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {dinosaurs[i].typename} Attack Power: {dinosaurs[i].attackPower}");
                }
                int selection;
                bool valid = false;
                do
                {
                    valid = Int32.TryParse(Console.ReadLine(), out selection);
                    if (valid)
                    {
                        valid = (selection > 0 && selection <= dinosaurs.Count);
                        selection--;
                    }
                } while (!valid);
                return dinosaurs[selection];
        }
        public Dinosaur ComputerChooseDinosaurToFight()
        {
            Dinosaur strongest = dinosaurs[0];
            Dinosaur dinoWithMostPower = dinosaurs[0];
            int largestAttackPower = 0;
            int largestPowerLevel = 0;
            foreach(Dinosaur dino in dinosaurs)
            {
                if(dino.attackPower >= largestAttackPower)
                {
                    largestAttackPower = dino.attackPower;
                    strongest = dino;
                }
                if(dino.powerLevel >= largestPowerLevel)
                {
                    largestPowerLevel = dino.powerLevel;
                    dinoWithMostPower = dino;
                }
            }
            if(dinoWithMostPower == null)
            {
                return strongest;
            }
            if (dinoWithMostPower == strongest)
            {
                return dinoWithMostPower;
            }
            else
            {
                return dinoWithMostPower;
            }
        }
        public bool CheckHasDied(Dinosaur dinosaur)
        {
            return dinosaur.health <= 0;
        }
        public void RemoveDinosaur(Dinosaur dinosaur)
        {
            if(dinosaurs.Contains(dinosaur))
            {
                dinosaurs.Remove(dinosaur);
            }
            else
            {
                throw new IndexOutOfRangeException();
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
    }
}
