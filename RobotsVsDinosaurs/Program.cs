using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set Window Properties
            Console.WindowHeight = 40;
            Console.WindowWidth = 120;

            Battlefield debugBattlefield = new Battlefield();

            debugBattlefield.RunBattle();


            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
