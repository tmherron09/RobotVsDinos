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
        bool isFleetTurn;
        bool hasWinner;



        public Battlefield()
        {
            fleet = new Fleet();
            herd = new Herd();
            rng = new Random();
        }


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
                Console.ReadLine();
                // Change whose turn it is.
                isFleetTurn = !isFleetTurn;
                // TODO Add WinningConditionMet method (herd.dinosaurs.Any() && fleet.robots.Any())
                hasWinner = WinningConditionMet();
            } while (!hasWinner);
            // Display Victory Information
            Console.ReadLine();
        }

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

        private void UpdateStatsDisplay()
        {
            string robotInfo = "Robot Stats\n";
            string dinoInfo = "Dinosaur Stats\n";
            Console.Clear();
            Console.SetCursorPosition(0, 20);
            foreach (Robot robot in fleet.robots)
            {
                robotInfo = string.Concat(robotInfo, $"{robot.name}\nHealth: {robot.health,3} Power Level: {robot.powerLevel}\n");
            }
            foreach (Dinosaur dino in herd.dinosaurs)
            {
                dinoInfo = string.Concat(dinoInfo, $"{dino.typename}\nHealth: {dino.health,3} Power Level: {dino.powerLevel}\n");
            }
            Console.WriteLine(robotInfo + "\n----------\n\n" + dinoInfo);
            Console.SetCursorPosition(0, 0);
        }

        public void InitializeBattleField()
        {

            fleet.InitializeFleet(100, 20, 30);
            herd.InitializeHerd(150, 30, 60, 40);
            ChooseYourTeam();

        }
        private void ChooseYourTeam()
        {
            bool valid = false;
            int userInput;
            Console.WriteLine("Please select your team: ");
            Console.WriteLine("1) Robots\n2) Dinosaurs");
            do
            {
                valid = Int32.TryParse(Console.ReadLine(), out userInput);
                if (valid)
                {
                    if (userInput == 1)
                    {
                        fleet.isHuman = true;
                        break;
                    }
                    else if (userInput == 2)
                    {
                        herd.isHuman = true;
                        break;
                    }
                    else
                    {
                        valid = false;
                    }
                }
                Console.WriteLine("Invalid choice. Please choose again.");

            } while (!valid);

        }
        private bool WinningConditionMet()
        {
            if (!fleet.robots.Any() || !herd.dinosaurs.Any())
            {
                return true;
            }
            return false;
        }
        public void DebugLogBattleField()
        {
            Console.WriteLine("Testing Fleet initialization.");
            if (fleet.robots.Count != 0)
            {
                Console.WriteLine($"Fleet count is {fleet.robots.Count}");
                foreach (Robot robot in fleet.robots)
                {
                    Console.WriteLine($"Robot name: {robot.name} Health: {robot.health} Power Level:{robot.powerLevel}\n Weapon Type: {robot.weapon.name} Weapon Attack Power: {robot.weapon.attackPower}");
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
            if (herd.dinosaurs.Count != 0)
            {
                Console.WriteLine($"Herd count is {herd.dinosaurs.Count}");
                foreach (Dinosaur dino in herd.dinosaurs)
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
        public void AttackPhase()
        {
            if (isFleetTurn && fleet.isHuman || !isFleetTurn && herd.isHuman)
            {
                HumanAttackAction();
            }
            else
            {
                ComputerAttackAction();
            }
        }
        public void HumanAttackAction()
        {
            int hitAmount;
            if (isFleetTurn)
            {
                if (!fleet.CheckIfRobotsCanFight())
                {
                    Console.WriteLine("No Robots have enough energy to fight.");
                    fleet.UpdatePowerLevels();
                    return;
                }
                Robot currentTurnRobot;
                Dinosaur targetDinosaur;
                // PLayer Chooses Robot
                currentTurnRobot = fleet.ChooseRobotToFight();
                // Player chooses weapon for Robot if not already assigned
                currentTurnRobot.InitializeWeapon(fleet);
                // Player ChoosesTargetDinosaur
                targetDinosaur = fleet.HumanChooseTargetDinosaur(herd);
                // Robot attacks and returns hit amount.
                hitAmount = currentTurnRobot.Attack(targetDinosaur);
                ReportRobotAttackedDinosaur(currentTurnRobot, targetDinosaur, hitAmount);
                fleet.UpdatePowerLevels(currentTurnRobot);
            }
            else
            {
                if (!herd.CheckIfDinosaursCanFight())
                {
                    Console.WriteLine("No Dinosaur has enough energy to fight.");
                    herd.UpdatePowerLevels();
                    return;
                }
                Dinosaur currentTurnDinosaur;
                Robot targetRobot;
                // Player chooses dinosaur
                currentTurnDinosaur = herd.ChooseDinosaurToFight();
                // Player chooses target Robot
                targetRobot = herd.HumanChooseTargetRobot(fleet);
                // Player choose attack type and gets hit amount.
                hitAmount = currentTurnDinosaur.HumanAttack(targetRobot, herd, rng);
                ReportDinosaurAttackedRobot(currentTurnDinosaur, targetRobot, hitAmount);
                herd.UpdatePowerLevels(currentTurnDinosaur);
            }
        }
        private void ComputerAttackAction()
        {
            int hitAmount;
            if (isFleetTurn)
            {
                if (!fleet.CheckIfRobotsCanFight())
                {
                    Console.WriteLine("No Robots have enough energy to fight.");
                    fleet.UpdatePowerLevels();
                    return;
                }
                Robot currentTurnRobot;
                Dinosaur targetDinosaur;
                // Computer chooses Robot
                currentTurnRobot = fleet.ComputerChooseRobotToFight();
                // Computer chooses weapon for Robot if not already assigned.
                currentTurnRobot.InitializeWeapon(fleet);
                // Computer Selects Target
                targetDinosaur = fleet.ComputeChooseTargetDinosaur(herd, rng);
                // Computer attacks and returns hit amount.
                hitAmount = currentTurnRobot.Attack(targetDinosaur);
                ReportRobotAttackedDinosaur(currentTurnRobot, targetDinosaur, hitAmount);
                fleet.UpdatePowerLevels(currentTurnRobot);
            }
            else
            {
                if (!herd.CheckIfDinosaursCanFight())
                {
                    Console.WriteLine("No Dinosaur has enough energy to fight.");
                    herd.UpdatePowerLevels();
                    return;
                }
                Dinosaur currentTurnDinosaur;
                Robot targetRobot;
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

        }

        public void ReportRobotAttackedDinosaur(Robot currentTurnRobot, Dinosaur targetDinosaur, int hitAmount)
        {
            string msg;
            bool isDead;
            if (hitAmount > 0)
            {
                msg = $"{currentTurnRobot.name} hit {targetDinosaur.typename} with {currentTurnRobot.weapon.name} for {hitAmount} damage!";
                isDead = herd.CheckHasDied(targetDinosaur);
                if (isDead)
                {
                    msg = string.Concat(msg + $"\t{targetDinosaur.typename} has been destroyed.");
                    herd.RemoveDinosaur(targetDinosaur);
                }
            }
            else
            {
                msg = $"{currentTurnRobot.name} MISSED {targetDinosaur.typename} with {currentTurnRobot.weapon.name}!";
            }
            UpdateStatsDisplay();
            Console.WriteLine("Robots Turn\n");
            Console.WriteLine(msg);
            
        }
        public void ReportDinosaurAttackedRobot(Dinosaur currentTurnDinosaur, Robot targetRobot, int hitAmount)
        {
            string msg;
            bool isDead;
            if (hitAmount > 0)
            {
                msg = $"{currentTurnDinosaur.typename} hit {targetRobot.name} for {hitAmount} damage!";
                isDead = fleet.CheckHasDied(targetRobot);
                if (isDead)
                {
                    msg = string.Concat(msg + $"\t{targetRobot.name} has been destroyed.");
                    fleet.RemoveRobot(targetRobot);
                }
            }
            else
            {
                msg = $"{currentTurnDinosaur.typename} has MISSED {targetRobot.name}!";
            }
            UpdateStatsDisplay();
            Console.WriteLine("Dinosaurs Turn\n");
            Console.WriteLine(msg);
            
        }




    }
}
