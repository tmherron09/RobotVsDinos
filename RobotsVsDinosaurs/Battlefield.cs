using System;
using System.Linq;

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
            if (!fleet.robots.Any() || !herd.dinosaurs.Any())
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
            foreach (Robot robot in fleet.robots)
            {
                robotInfo = string.Concat(robotInfo, $"{robot.name}\nHealth: {robot.health,3} Power Level: {robot.powerLevel}\n");
            }
            foreach (Dinosaur dino in herd.dinosaurs)
            {
                dinoInfo = string.Concat(dinoInfo, $"{dino.name}\nHealth: {dino.health,3} Power Level: {dino.powerLevel}\n");
            }
            Console.WriteLine(robotInfo + "\n----------\n\n" + dinoInfo);
            Console.SetCursorPosition(0, 0);
        }
        private void DisplayWinner()
        {
            string msg = "";
            if (fleet.robots.Any())
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
            fleet.InitializeFleet(100, 20, 30);
            herd.InitializeHerd(125, 30, 40, 40);
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
            if (isFleetTurn && fleet.isHuman || !isFleetTurn && herd.isHuman)
            {
                HumanAttackActionCall();
            }
            else
            {
                ComputerAttackActionCall();
            }
        }
        /* Human Player Branch */
        // Calls for Human Robot attack branch or Dinosaur Attack branch.
        public void HumanAttackActionCall()
        {
            if (isFleetTurn)
            {
                HumanRobotAttackAction();
            }
            else
            {
                HumanDinosaurAttackAction();
            }
        }
        // Human Robot Attack Turn steps
        private void WarriorAttackAction()
        {
            Army currentArmy;
            Warrior currentTurnWarrior;
            Warrior targetDinosaur;
            if (isFleetTurn)
            {
                currentArmy = fleet;
               

            }
            else
            {
                currentArmy = herd;
            }
            if(!currentArmy.CheckIfWarriorsCanFight())
            {
                Console.WriteLine($"No {currentArmy.warriorType} have enough energy to fight.");
                fleet.UpdatePowerLevels();
                return;
            }
            currentTurnWarrior = currentArmy.HumanChooseWarriorToFight(this, currentArmy);
            UpdateStatsDisplay();
            if(currentArmy.GetType() == typeof(Fleet))
            {
                currentTurnWarrior.InitializeWeapon(fleet, this);
            }
        }
        private void HumanRobotAttackAction()
        {
            int hitAmount;
            if (!fleet.CheckIfRobotsCanFight())
            {
                Console.WriteLine("No Robots have enough energy to fight.");
                fleet.UpdatePowerLevels();
                return;
            }
            Warrior currentTurnRobot;
            Warrior targetDinosaur;
            // PLayer Chooses Robot
            currentTurnRobot = fleet.HumanChooseRobotToFight(this);
            UpdateStatsDisplay();
            // Player chooses weapon for Robot if not already assigned
            currentTurnRobot.InitializeWeapon(fleet, this);
            UpdateStatsDisplay();
            // Player ChoosesTargetDinosaur
            targetDinosaur = fleet.HumanChooseTargetDinosaur(herd, this);
            UpdateStatsDisplay();
            // Robot attacks and returns hit amount.
            hitAmount = currentTurnRobot.Attack(targetDinosaur);
            ReportRobotAttackedDinosaur(currentTurnRobot, targetDinosaur, hitAmount);
            fleet.UpdatePowerLevels(currentTurnRobot);
        }
        // Human Dinosaur Attack Turn steps
        private void HumanDinosaurAttackAction()
        {
            int hitAmount;
            if (!herd.CheckIfDinosaursCanFight())
            {
                Console.WriteLine("No Dinosaur has enough energy to fight.");
                herd.UpdatePowerLevels();
                return;
            }
            Dinosaur currentTurnDinosaur;
            Robot targetRobot;
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
        //private void ComputerAttackActionCall()
        //{

        //    if (isFleetTurn)
        //    {
        //        ComputerRobotAttackAction();
        //    }
        //    else
        //    {
        //        ComputerDinosaurAttackAction();
        //    }

        //}
        // Computer Robot Attack Turn Logic
        //private void ComputerRobotAttackAction()
        //{
        //    int hitAmount;
        //    if (!fleet.CheckIfRobotsCanFight())
        //    {
        //        Console.WriteLine("No Robots have enough energy to fight.");
        //        fleet.UpdatePowerLevels();
        //        return;
        //    }
        //    Warrior currentTurnRobot;
        //    Warrior targetDinosaur;
        //    // Computer chooses Robot
        //    currentTurnRobot = fleet.ComputerChooseRobotToFight(rng);
        //    // Computer Selects Target
        //    targetDinosaur = fleet.ComputeChooseTargetDinosaur(herd, rng);
        //    // Computer attacks and returns hit amount.
        //    hitAmount = currentTurnRobot.Attack(targetDinosaur);
        //    ReportRobotAttackedDinosaur(currentTurnRobot, targetDinosaur, hitAmount);
        //    fleet.UpdatePowerLevels(currentTurnRobot);
        //}
        // Computer Dinosaur Attack Turn Logic
        private void ComputerDinosaurAttackAction()
        {
            int hitAmount;
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
        #endregion

        #region Attack Reports
        // Creates a msg based on Result of Robots turn.
        public void ReportRobotAttackedDinosaur(Robot currentTurnRobot, Dinosaur targetDinosaur, int hitAmount)
        {
            string msg;
            bool isDead;
            if (hitAmount > 0)
            {
                msg = $"{currentTurnRobot.name} hit {targetDinosaur.name} with {currentTurnRobot.weapon.name} for {hitAmount} damage!";
                isDead = herd.CheckHasDied(targetDinosaur);
                if (isDead)
                {
                    msg = string.Concat(msg + $"\t{targetDinosaur.name} has been destroyed.");
                    herd.RemoveDinosaur(targetDinosaur);
                }
            }
            else
            {
                msg = $"{currentTurnRobot.name} MISSED {targetDinosaur.name} with {currentTurnRobot.weapon.name}!";
            }
            UpdateStatsDisplay();
            Console.WriteLine("Robots Turn\n");
            Console.WriteLine(msg);

        }
        // Creates a msg based on Result of Dinosaurs turn.
        public void ReportDinosaurAttackedRobot(Dinosaur currentTurnDinosaur, Robot targetRobot, int hitAmount)
        {
            string msg;
            bool isDead;
            if (hitAmount > 0)
            {
                msg = $"{currentTurnDinosaur.name} hit {targetRobot.name} for {hitAmount} damage!";
                isDead = fleet.CheckHasDied(targetRobot);
                if (isDead)
                {
                    msg = string.Concat(msg + $"\t{targetRobot.name} has been destroyed.");
                    fleet.RemoveRobot(targetRobot);
                }
            }
            else
            {
                msg = $"{currentTurnDinosaur.name} has MISSED {targetRobot.name}!";
            }
            UpdateStatsDisplay();
            Console.WriteLine("Dinosaurs Turn\n");
            Console.WriteLine(msg);

        }
        #endregion

        #region Depreciated
        // Depreciated DebugLog
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
                    Console.WriteLine($"Dino Type: {dino.name} Dino Health: {dino.health} Dino Power Level: {dino.health} Dino Attack Power: {dino.attackPower}");
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
