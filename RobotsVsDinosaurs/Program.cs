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
            Console.WindowHeight = 44;
            Console.WindowWidth = 120;

            // Game wrapper for replaying the game.
            GameSession game = new GameSession();
            game.PlayGame();

            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
