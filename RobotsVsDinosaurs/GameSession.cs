using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class GameSession
    {
        Battlefield battlefield;

        public void PlayGame()
        {
            do
            {
                battlefield = new Battlefield();
                battlefield.RunBattle();
            } while (PlayAgain());
        }
        public bool PlayAgain()
        {

            string msg = "Would you like to play again? (y/n): ";
            string userChoice;
            while (true)
            {
                Console.Clear();
                Console.Write(msg);
                userChoice = Console.ReadLine().ToLower();
                if (userChoice == "y" || userChoice == "yes")
                {
                    return true;
                }
                else if (userChoice == "n" || userChoice == "no")
                {
                    return false;
                }
            }
        }

    }
}
