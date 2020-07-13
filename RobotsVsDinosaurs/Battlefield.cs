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



        public Battlefield()
        {
            fleet = new Fleet();
            herd = new Herd();
            rng = new Random();
        }

        public void InitializeBattleField()
        {
            fleet.InitializeFleet(rng);
            herd.InitializeHerd(rng);
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
            InitializeBattleField();
            do
            {
                if(isFleetTurn)
                {
                    if(fleet.isHuman)
                    {
                        // Human fleet turn
                        RobotAttackPhase();
                    }
                    else
                    {
                        // Computer as fleet turn.
                        RobotAttackPhase();
                    }
                }
                else
                {
                    if(herd.isHuman)
                    {
                        // human herd turn
                        HerdAttackPhase();
                    }
                    else
                    {
                        // Computer as herd turn.
                        HerdAttackPhase();
                    }
                }

                isFleetTurn = !isFleetTurn;

            } while (herd.dinosaurs.Any() && fleet.robots.Any());
            

            Console.ReadLine();
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
        public void RobotAttackPhase()
        {
            Robot currentTurnRobot;
            if (fleet.isHuman)
            {
                currentTurnRobot = fleet.ChooseRobotToFight();
                AttackAction(currentTurnRobot, herd);
            }
            else
            {
                currentTurnRobot = fleet.ComputerChooseRobotToFight();
                ComputerAttackAction(currentTurnRobot, herd);
            }
        }

        public void AttackAction(Robot robot, Herd herd)
        {
            // Choose Weapon
            robot.ChooseWeapon();
            //Choose Target
            for (int i = 1; i <= herd.dinosaurs.Count; i++)
            {
                Console.WriteLine($"{i}: {herd.dinosaurs[i-1].typename}");
            }
            int targetPosition;
            bool valid = false;
            do
            {
                valid = Int32.TryParse(Console.ReadLine(), out targetPosition);
                if(valid)
                {
                    valid = (targetPosition > 0 && targetPosition <= herd.dinosaurs.Count);
                    targetPosition--;
                }
            } while (!valid);
            int hitAmount = robot.Attack(herd.dinosaurs[targetPosition]);
            // DisplayAttackInformation()
            //PlaceHolder
            Console.WriteLine($"{robot.name} hit {herd.dinosaurs[targetPosition].typename} with {robot.weapon.name} for {hitAmount} damage!");
            if(herd.CheckHasDied(herd.dinosaurs[targetPosition]))
            {
                //Display Death Message
                Console.WriteLine($"{herd.dinosaurs[targetPosition].typename} has been destroyed.");
                herd.RemoveDinosaur(herd.dinosaurs[targetPosition]);
            }
        }
        private void ComputerAttackAction(Robot currentTurnRobot, Herd herd)
        {
            throw new NotImplementedException();
        }
        public void HerdAttackPhase()
        {
            Dinosaur currentTurnDinosaur;
            if(herd.isHuman)
            {
                currentTurnDinosaur = herd.ChooseDinosaurToFight();
                AttackAction(currentTurnDinosaur, fleet);
            }
            else
            {
                currentTurnDinosaur = herd.ComputerChooseDinosaurToFight();
                ComputerAttackAction(currentTurnDinosaur, fleet);
            }
        }
        public void AttackAction(Dinosaur dinosaur, Fleet fleet)

        {
            // Choose Ability
            //Choose Target
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
            int hitAmount = dinosaur.Attack(fleet.robots[targetPosition]);
            // DisplayAttackInformation()
            //PlaceHolder
            Console.WriteLine($"{dinosaur.typename} hit {fleet.robots[targetPosition].name} for {hitAmount} damage!");
            if (fleet.CheckHasDied(fleet.robots[targetPosition]))
            {
                //Display Death Message
                Console.WriteLine($"{fleet.robots[targetPosition].name} has been destroyed.");
                fleet.RemoveRobot(fleet.robots[targetPosition]);
            }
        }
        private void ComputerAttackAction(Dinosaur currentTurnDinosaur, Fleet fleet)
        {
            throw new NotImplementedException();
        }
    }
}
