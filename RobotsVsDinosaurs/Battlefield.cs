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
    }
}
