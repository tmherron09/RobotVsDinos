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


        public Battlefield()
        {
            fleet = new Fleet();
            herd = new Herd();
        }

        public void InitializeBattleField()
        {
            fleet.InitializeFleet();
            herd.InitializeHerd();
        }

        public void RunBattle()
        {
            InitializeBattleField();

        }

        public void DebugLogBattleField()
        {
            Console.WriteLine("Testing Fleet initialization.");
            if(fleet.robots.Count == 0)
            {
                Console.WriteLine("Fleet not initialized");
            }

        }
    }
}
