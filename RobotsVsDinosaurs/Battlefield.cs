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

        public void InitializeBattleField()
        {
            fleet.InitializeFleet(rng);
            herd.InitializeHerd();
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
        public void RunBattle()
        {
            hasWinner = false;
            InitializeBattleField();
            do
            {
                // Start turn
                AttackPhase();
                // Change whose turn it is.
                isFleetTurn = !isFleetTurn;
                // TODO Add WinningConditionMet method (herd.dinosaurs.Any() && fleet.robots.Any())
                hasWinner = WinningConditionMet();
            } while (!hasWinner);
            // Display Victory Information
            Console.ReadLine();
        }
        private bool WinningConditionMet()
        {
            // Check if either List of Robot or Dino is Empty or all null
            throw new NotImplementedException();
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
                // TODO: RobotAttackedDinosaur()
            }
            else
            {
                Dinosaur currentTurnDinosaur;
                Robot targetRobot;
                // Player chooses dinosaur
                currentTurnDinosaur = herd.ChooseDinosaurToFight();
                // Player chooses target Robot
                targetRobot = herd.HumanChooseTargetRobot(fleet);
                // Player choose attack type and gets hit amount.
                hitAmount = currentTurnDinosaur.HumanAttack(targetRobot, herd, rng);
                // DinosaurAttackedRobot()
            }
        }
        private void ComputerAttackAction()
        {
            int hitAmount;
            if (isFleetTurn)
            {
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
                // TODO: RobotAttackedDinosaur()
            }
            else
            {
                Dinosaur currentTurnDinosaur;
                Robot targetRobot;
                // Computer chooses dinosaur
                currentTurnDinosaur = herd.ComputerChooseDinosaurToFight();
                // Computer chooses target
                targetRobot = herd.ComputerChooseTargetRobot(fleet, rng);
                // Computer chooses Attack Type
                // ComputerChooseAttackType(targetRobot) and returns hit amount.
                hitAmount = currentTurnDinosaur.ComputerChooseAttackType(targetRobot, herd, rng);
                // DinosaurAttackedRobot()
            }

        }
        // Depreciated Attack action: TODO Refactor in Human/ComputerAttackAction and HitCalculation methods.
        public void AttackAction(Robot robot, Herd herd)
        {
            //Choose Target
            for (int i = 1; i <= herd.dinosaurs.Count; i++)
            {
                Console.WriteLine($"{i}: {herd.dinosaurs[i - 1].typename}");
            }
            int targetPosition;
            bool valid = false;
            do
            {
                valid = Int32.TryParse(Console.ReadLine(), out targetPosition);
                if (valid)
                {
                    valid = (targetPosition > 0 && targetPosition <= herd.dinosaurs.Count);
                    targetPosition--;
                }
            } while (!valid);
            int hitAmount = robot.Attack(herd.dinosaurs[targetPosition]);
            // DisplayAttackInformation()
            //PlaceHolder
            Console.WriteLine($"{robot.name} hit {herd.dinosaurs[targetPosition].typename} with {robot.weapon.name} for {hitAmount} damage!");
            if (herd.CheckHasDied(herd.dinosaurs[targetPosition]))
            {
                //Display Death Message
                Console.WriteLine($"{herd.dinosaurs[targetPosition].typename} has been destroyed.");
                herd.RemoveDinosaur(herd.dinosaurs[targetPosition]);
            }
        }
        // Depreciated Attack action: TODO Refactor in Human/ComputerAttackAction and HitCalculation methods.
        public void AttackAction(Dinosaur dinosaur, Fleet fleet)

        {
            Console.WriteLine("Select a robot to attack: ");
            for (int i = 1; i <= fleet.robots.Count; i++)
            {
                Console.WriteLine($"{i}: {fleet.robots[i-1].name}");
            }

            int targetPosition;
            bool valid = false;
            do
            {
                valid = Int32.TryParse(Console.ReadLine(), out targetPosition);
                if (valid)
                {
                    valid = (targetPosition > 0 && targetPosition <= fleet.robots.Count);
                    targetPosition--;
                }

            } while (!valid);
            int hitAmount = dinosaur.HumanAttack(fleet.robots[targetPosition], herd, rng);
            // DisplayAttackInformation()
            //PlaceHolder
            if (hitAmount > 0)
            {
                Console.WriteLine($"{dinosaur.typename} hit {fleet.robots[targetPosition].name} for {hitAmount} damage!");
                if (fleet.CheckHasDied(fleet.robots[targetPosition]))
                {
                    //Display Death Message
                    Console.WriteLine($"{fleet.robots[targetPosition].name} has been destroyed.");
                    fleet.RemoveRobot(fleet.robots[targetPosition]);
                }
            }
            else
            {
                Console.WriteLine($"{dinosaur.typename} has MISSED!");
            }
        }
    }
}
