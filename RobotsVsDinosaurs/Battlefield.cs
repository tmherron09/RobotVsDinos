﻿using System;
using System.Linq;

namespace RobotsVsDinosaurs
{
    public class Battlefield
    {
        Army PlayerOneArmy;
        Army PlayerTwoArmy;
        Fleet fleet;
        Herd herd;
        Random rng;
        bool isPlayerOneTurn = true;
        bool isPlayerTwoHuman = false;
        bool isFleetTurn;
        bool hasWinner;

        public Battlefield()
        {
            fleet = new Fleet();
            herd = new Herd();
            rng = new Random();
        }

        #region Main Gam Loop
        // The main Game Loop
        public void RunBattle()
        {
            hasWinner = false;
            InitializeBattleField();
            do
            {
                //Display whose turn and Current stats.
                DisplayTurnInfo();
                // Start turn
                AttackPhase();
                Console.ReadKey();
                // Change whose turn it is.
                isFleetTurn = !isFleetTurn;
                // TODO Add WinningConditionMet method (herd.dinosaurs.Any() && fleet.robots.Any())
                hasWinner = WinningConditionMet();
            } while (!hasWinner);
            // Display Victory Information
            DisplayWinner();
        }
        // Check if either team List is empty/All units destroyed.
        private bool WinningConditionMet()
        {
            if (!fleet.Warriors.Any() || !herd.Warriors.Any())
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Display Methods
        // Temporary method for displaying whose turn it is and updating stats at beginning of each turn.
        private void DisplayTurnInfo()
        {
            UpdateStatsDisplay();
            if (isFleetTurn)
            {
                Console.WriteLine("Robots Turn\n");
            }
            else
            {
                Console.WriteLine("Dinosaurs turn\n");
            }
        }
        // Temporary method to write stats of Robots and Dinosaurs to lower lines on console.
        public void UpdateStatsDisplay()
        {
            string robotInfo = "Robot Stats\n";
            string dinoInfo = "Dinosaur Stats\n";
            Console.Clear();
            Console.SetCursorPosition(0, 22);
            foreach (Robot robot in fleet.Warriors)
            {

                //TODO: Replace with Warrior.DisplayStats
                robotInfo = string.Concat(robotInfo, $"{robot.Name}\nHealth: {robot.Health,3} Power Level: {robot.Stamina}\n");
            }
            foreach (Dinosaur dino in herd.Warriors)
            {
                dinoInfo = string.Concat(dinoInfo, $"{dino.Name}\nHealth: {dino.Health,3} Power Level: {dino.Stamina}\n");
            }
            Console.WriteLine(robotInfo + "\n----------\n\n" + dinoInfo);
            Console.SetCursorPosition(0, 0);
        }
        private void DisplayWinner()
        {
            string msg = "";
            if (fleet.Warriors.Any())
            {
                Console.WriteLine("Robots Win!");
            }
            else
            {
                Console.WriteLine("Dinosaurs Win!");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        #endregion

        #region Initialize Game Methods
        // Calls methods to create Fleet and Herd
        public void InitializeBattleField()
        {
            ChooseYourTeam();
            fleet.InitializeArmy(100, 20, 30, 0);
            herd.InitializeArmy(125, 30, 40, 40);
        }
        // Asks Player to choose a team. TODO: Add two player.
        private void ChooseYourTeam()
        {
            bool valid = false;
            int userInput;
            do
            {
                Console.WriteLine("Please select your team: ");
                Console.WriteLine("1) Robots\n2) Dinosaurs");
                valid = Int32.TryParse(Console.ReadLine(), out userInput);
                if (valid)
                {
                    if (userInput == 1)
                    {
                        PlayerOneArmy = fleet;
                        PlayerTwoArmy = herd;
                        fleet.IsHuman = true;
                        break;
                    }
                    else if (userInput == 2)
                    {
                        PlayerOneArmy = herd;
                        PlayerTwoArmy = fleet;
                        herd.IsHuman = true;
                        break;
                    }
                    else
                    {
                        valid = false;
                    }
                }
                Console.WriteLine("Invalid choice. Please choose again.");
                if (!valid)
                {
                    Console.Clear();
                }
            } while (!valid);

        }
        #endregion

        #region Attack Methods
        // First part of turn, calls method depending on if Human player or Computer Player
        public void AttackPhase()
        {
            if (isPlayerOneTurn || isPlayerTwoHuman)
            {
                HumanWarriorAttackAction();
            }
            else
            {
                ComputerAttackActionCall();
            }
        }

        private void HumanWarriorAttackAction()
        {
            Army HumanArmy = isPlayerOneTurn ? PlayerOneArmy : PlayerTwoArmy;
            Warrior attacker;
            Warrior defender;

            if (!HumanArmy.CheckIfWarriorsCanFight())
            {
                Console.WriteLine($"No {HumanArmy.GetArmyWarriorType()} have enough energy to fight.");
                fleet.UpdatePowerLevels();
                return;
            }
            attacker = HumanChooseAttacker();
            if(!attacker.IsInitialized)
            {
                attacker.InitializeWarriors();
            }
            UpdateStatsDisplay();
            defender = HumanChooseDefender();
            UpdateStatsDisplay();



        }


        // Human Dinosaur Attack Turn steps
        private void HumanDinosaurAttackAction()
        {
            float hitAmount;
            if (!herd.CheckIfDinosaursCanFight())
            {
                Console.WriteLine("No Dinosaur has enough energy to fight.");
                herd.UpdatePowerLevels();
                return;
            }
            Warrior currentTurnDinosaur;
            Warrior targetRobot;
            // Player chooses dinosaur
            currentTurnDinosaur = herd.HumanChooseDinosaurToFight(this);
            UpdateStatsDisplay();
            // Player chooses target Robot
            targetRobot = herd.HumanChooseTargetRobot(fleet, this);
            UpdateStatsDisplay();
            // Player choose attack type and gets hit amount.
            hitAmount = currentTurnDinosaur.HumanChooseAttackType(targetRobot, rng, this);
            ReportDinosaurAttackedRobot(currentTurnDinosaur, targetRobot, hitAmount);
            herd.UpdatePowerLevels(currentTurnDinosaur);
        }
        /* Computer Player Branch */
        // Calls methods to run Computer turn. Both Robot/Dinosaur.
        private void ComputerAttackActionCall()
        {

            if (isFleetTurn)
            {
                ComputerRobotAttackAction();
            }
            else
            {
                ComputerDinosaurAttackAction();
            }

        }
        // Computer Robot Attack Turn Logic
        private void ComputerRobotAttackAction()
        {
            float hitAmount;
            if (!fleet.CheckIfRobotsCanFight())
            {
                Console.WriteLine("No Robots have enough energy to fight.");
                fleet.UpdatePowerLevels();
                return;
            }
            Warrior currentTurnRobot;
            Warrior targetDinosaur;
            // Computer chooses Robot
            currentTurnRobot = fleet.ComputerChooseRobotToFight(rng);
            // Computer Selects Target
            targetDinosaur = fleet.ComputeChooseTargetDinosaur(herd, rng);
            // Computer attacks and returns hit amount.
            hitAmount = currentTurnRobot.Attack(targetDinosaur);
            ReportRobotAttackedDinosaur(currentTurnRobot, targetDinosaur, hitAmount);
            fleet.UpdatePowerLevels(currentTurnRobot);
        }
        // Computer Dinosaur Attack Turn Logic
        private void ComputerDinosaurAttackAction()
        {
            float hitAmount;
            if (!herd.CheckIfDinosaursCanFight())
            {
                Console.WriteLine("No Dinosaur has enough energy to fight.");
                herd.UpdatePowerLevels();
                return;
            }
            Warrior currentTurnDinosaur;
            Warrior targetRobot;
            // Computer chooses dinosaur
            currentTurnDinosaur = herd.ComputerChooseDinosaurToFight();
            // Computer chooses target
            targetRobot = herd.ComputerChooseTargetRobot(fleet, rng);
            // Computer chooses Attack Type
            // ComputerChooseAttackType(targetRobot) and returns hit amount.
            hitAmount = currentTurnDinosaur.ComputerChooseAttackType(targetRobot, herd, rng);
            ReportDinosaurAttackedRobot(currentTurnDinosaur, targetRobot, hitAmount);
            herd.UpdatePowerLevels(currentTurnDinosaur);
        }
        #endregion

        #region Attack Reports
        // Creates a msg based on Result of Robots turn.
        public void ReportRobotAttackedDinosaur(Warrior currentTurnRobot, Warrior targetDinosaur, float hitAmount)
        {
            string msg;
            bool isDead;
            if (hitAmount > 0)
            {
                msg = $"{currentTurnRobot.Name} hit {targetDinosaur.Name} with {(currentTurnRobot as Robot).weapon.name} for {hitAmount} damage!";
                isDead = herd.CheckHasDied(targetDinosaur);
                if (isDead)
                {
                    msg = string.Concat(msg + $"\t{targetDinosaur.Name} has been destroyed.");
                    herd.RemoveDinosaur(targetDinosaur);
                }
            }
            else
            {
                msg = $"{currentTurnRobot.Name} MISSED {targetDinosaur.Name} with {(currentTurnRobot as Robot).weapon.name}!";
            }
            UpdateStatsDisplay();
            Console.WriteLine("Robots Turn\n");
            Console.WriteLine(msg);

        }
        // Creates a msg based on Result of Dinosaurs turn.
        public void ReportDinosaurAttackedRobot(Warrior currentTurnDinosaur, Warrior targetRobot, float hitAmount)
        {
            string msg;
            bool isDead;
            if (hitAmount > 0)
            {
                msg = $"{currentTurnDinosaur.Name} hit {targetRobot.Name} for {hitAmount} damage!";
                isDead = fleet.CheckHasDied(targetRobot);
                if (isDead)
                {
                    msg = string.Concat(msg + $"\t{targetRobot.Name} has been destroyed.");
                    fleet.RemoveRobot(targetRobot);
                }
            }
            else
            {
                msg = $"{currentTurnDinosaur.Name} has MISSED {targetRobot.Name}!";
            }
            UpdateStatsDisplay();
            Console.WriteLine("Dinosaurs Turn\n");
            Console.WriteLine(msg);

        }
        #endregion


        #region Human Turn Decisions

        public Warrior HumanChooseAttacker()
        {
            Army attackingArmy = isPlayerOneTurn ? PlayerOneArmy : PlayerTwoArmy;
            return HumanChooseWarrior(attackingArmy);
        }
        public Warrior HumanChooseDefender()
        {
            Army defendingArmy = isPlayerOneTurn ? PlayerTwoArmy : PlayerOneArmy;
            return HumanChooseWarrior(defendingArmy);
        }


        public Warrior HumanChooseWarrior(Army army)
        {
            int selection;
            bool valid;

            do
            {
                Console.WriteLine($"Please select which {army.GetArmyWarriorType()} to use: ");

                for (int i = 0; i < army.Warriors.Count; i++)
                {
                    Console.WriteLine(army.Warriors[i].ChoiceDisplayMessage(i));
                }

                valid = Int32.TryParse(Console.ReadLine(), out selection);
                if (valid)
                {
                    valid = (selection > 0 && selection <= army.Warriors.Count);
                    selection--;
                    if (valid && army.Warriors[selection].Stamina <= 0)
                    {
                        valid = false;
                        Console.WriteLine($"{army.Warriors[selection].Name} has no power to fight.");
                    }
                }
                if (!valid)
                {
                    UpdateStatsDisplay();
                }
            } while (!valid);
            return army.Warriors[selection];

        }

        #endregion

        #region ComputerTurn Decisions

        #endregion




        #region Depreciated
        // Depreciated DebugLog
        public void DebugLogBattleField()
        {
            Console.WriteLine("Testing Fleet initialization.");
            if (fleet.Warriors.Count != 0)
            {
                Console.WriteLine($"Fleet count is {fleet.Warriors.Count}");
                foreach (Robot robot in fleet.Warriors)
                {
                    Console.WriteLine($"Robot name: {robot.Name} Health: {robot.Health} Power Level:{robot.Stamina}\n Weapon Type: {robot.weapon.name} Weapon Attack Power: {robot.weapon.attackPower}");
                }
                foreach (Weapon weapon in fleet.availableWeapons)
                {
                    Console.WriteLine($"Weapon Name: {weapon.name} Weapon Attack Power: {weapon.attackPower}");
                }
            }
            else
            {
                Console.WriteLine("Fleet not initialized");
            }
            if (herd.Warriors.Count != 0)
            {
                Console.WriteLine($"Herd count is {herd.Warriors.Count}");
                foreach (Dinosaur dino in herd.Warriors)
                {
                    Console.WriteLine($"Dino Type: {dino.Name} Dino Health: {dino.Health} Dino Power Level: {dino.Health} Dino Attack Power: {dino.AttackPower}");
                }
            }
            else
            {

                Console.WriteLine("Herd not initialized correctly.");
            }
            Console.WriteLine("End of Debug.");

        }
        #endregion
    }
}
