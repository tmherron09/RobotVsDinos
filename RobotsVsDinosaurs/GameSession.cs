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

        #region Logo Text
        string logo = @"  _____       _           _    __      __  _____  _                                      
 |  __ \     | |         | |   \ \    / / |  __ \(_)                                     
 | |__) |___ | |__   ___ | |_ __\ \  / /__| |  | |_ _ __   ___  ___  __ _ _   _ _ __ ___ 
 |  _  // _ \| '_ \ / _ \| __/ __\ \/ / __| |  | | | '_ \ / _ \/ __|/ _` | | | | '__/ __|
 | | \ \ (_) | |_) | (_) | |_\__ \\  /\__ \ |__| | | | | | (_) \__ \ (_| | |_| | |  \__ \
 |_|  \_\___/|_.__/ \___/ \__|___/ \/ |___/_____/|_|_| |_|\___/|___/\__,_|\__,_|_|  |___/
                                                                                         
                                                                                         ";
        #endregion

        // Start the Battle and reset battle on PlayAgain.
        public void PlayGame()
        {
            do
            {
                DisplayGameRules();
                battlefield = new Battlefield();
                battlefield.RunBattle();
            } while (PlayAgain());
        }
        // Quickly display information to help the player.
        public void DisplayGameRules()
        {
            string gameInfo = @"
Welcome to Robots Vs Dinosaurs!

Robots face off against dinosaurs on a battlefield.
You, the player, will choose a team to help.
Each team has its strengths and weaknesses.

Robots:
Robots get to choose one of 8 weapons at the start of their first turn.
There is only one of each weapon.
Robots never miss! But they have less energy and health due to the calculations.

Dinosaurs:
Dinosaurs can choose from one of three attacks.
Each attack has a % Chance of hitting the opponent.
Each attack also has a Bonus Modifier to their attack value.
Dinosaurs have more energy and health but are prone to missing.

Rounds:
After each the attack, the attacker's power level will decrease by 10,
while their teammates regain 10 power.
If the unit has no energy they cannot attack.
If a unit's health falls to 0, they are destroyed.
Last Team left standing wins!

Press any key to continue...";
            Console.WriteLine(logo);
            Console.Write(gameInfo);
            Console.ReadKey();
            Console.Clear();
        }


        // Allow player to play again.
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
