using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Battlefield
    {
        Fleet fleet;
        Herd herd;
        Random rng;


        public Battlefield()
        {
            fleet = new Fleet();
            herd = new Herd();
            rng = new Random();
        }

        public void InitializeBattleField()
        {
            fleet.InitializeFleet(rng);
            herd.InitializeHerd(rng);
        }

        public void RunBattle()
        {
            InitializeBattleField();
            
        }

        public void DebugLogBattleField()
        {
            Console.WriteLine("Testing Fleet initialization.");
            if(fleet.robots.Count != 0)
            {
                Console.WriteLine($"Fleet count is {fleet.robots.Count}");
                foreach(Robot robot in fleet.robots)
                {
                    Console.WriteLine($"Robot name: {robot.name} Health: {robot.health} Power Level:{robot.powerLevel}\n Weapon Type: {robot.weapon.name} Weapon Attack Power: {robot.weapon.attackPower}");
                }
                foreach(Weapon weapon in fleet.availableWeapons)
                {
                    Console.WriteLine($"Weapon Name: {weapon.name} Weapon Attack Power: {weapon.attackPower}");
                }
            }
            else
            {
                Console.WriteLine("Fleet not initialized");
            }
            if (herd.dinosaurs.Count != 0)
            {
                Console.WriteLine($"Herd count is {herd.dinosaurs.Count}");
                foreach(Dinosaur dino in herd.dinosaurs)
                {
                    Console.WriteLine($"Dino Type: {dino.typename} Dino Health: {dino.health} Dino Power Level: {dino.health} Dino Attack Power: {dino.attackPower}");
                }
            }
            else
            {
                
                Console.WriteLine("Herd not initialized correctly.");
            }
            Console.WriteLine("End of Debug.");

        }

        public void AttackAction(Robot robot, Herd herd)
        {
            // Choose Weapon
            //Choose Target
            for (int i = 1; i <= herd.dinosaurs.Count; i++)
            {
                Console.WriteLine($"{i}: {herd.dinosaurs[i].typename}");
            }

            int targetPosition;
            bool valid = false;
            do
            {
                valid = Int32.TryParse(Console.ReadLine(), out targetPosition);
                if(valid)
                {
                    valid = (targetPosition > 0 && targetPosition <= herd.dinosaurs.Count);
                }

            } while (!valid);
            int hitAmount = robot.Attack(herd.dinosaurs[targetPosition]);
            // DisplayAttackInformation()
            //PlaceHolder
            Console.WriteLine($"{robot.name} hit {herd.dinosaurs[targetPosition]} for {hitAmount} damage!");
        }
        public void AttackAction(Dinosaur dinosaur, Fleet fleet)
        {
            // Choose Ability
            //Choose Target
            for (int i = 1; i <= fleet.robots.Count; i++)
            {
                Console.WriteLine($"{i}: {fleet.robots[i].name}");
            }

            int targetPosition;
            bool valid = false;
            do
            {
                valid = Int32.TryParse(Console.ReadLine(), out targetPosition);
                if (valid)
                {
                    valid = (targetPosition > 0 && targetPosition <= fleet.robots.Count);
                }

            } while (!valid);
            int hitAmount = dinosaur.Attack(fleet.robots[targetPosition]);
            // DisplayAttackInformation()
            //PlaceHolder
            Console.WriteLine($"{dinosaur.typename} hit {fleet.robots[targetPosition]} for {hitAmount} damage!");
        }
    }
}
