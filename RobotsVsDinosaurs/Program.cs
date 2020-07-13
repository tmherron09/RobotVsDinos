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
            Battlefield debugBattlefield = new Battlefield();

            debugBattlefield.RunBattle();


            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
