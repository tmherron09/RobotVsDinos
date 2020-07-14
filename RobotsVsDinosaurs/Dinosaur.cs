using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Dinosaur
    {
        public string typename;
        public int health;
        public int powerLevel;
        public int attackPower;
        public string[] attackTypes;
        public double[] attackTypesModifiers;
        public int[] attackTypeModifierHitChance;

        public Dinosaur(string typeName)
        {
            this.typename = typeName;
            health = 100;
            powerLevel = 20; // stamina
            attackPower = 30;
            attackTypes = new string[] { "Scratch", "Bite", "Tail Whip" };
            attackTypesModifiers = new double[] { 1.25, 2.00, 1.00 };
            attackTypeModifierHitChance = new int[] { 80, 50, 100 };
        }

        public int HumanAttack(Robot targetRobot, Herd herd, Random rng)
        {
            int hitAmount = 0;

            Console.WriteLine("Choose an Attack: ");
            for (int i = 0; i < attackTypes.Length; i++)
            {
                Console.WriteLine($"{i + 1}) {attackTypes[i]} Hit Chance: {attackTypeModifierHitChance[i]}%  Bonus: x{attackTypesModifiers[i]} damage");
            }
            int selection;
            bool valid = false;
            do
            {
                valid = Int32.TryParse(Console.ReadLine(), out selection);
                if (valid)
                {
                    valid = (selection > 0 && selection <= attackTypes.Length);
                    selection--;
                }
            } while (!valid);
            if (rng.Next(101) < attackTypeModifierHitChance[selection])
            {
                hitAmount = (int)(attackPower * attackTypesModifiers[selection]);
            }
            return targetRobot.GetHit(hitAmount);
        }
        public int GetHit(Weapon weapon)
        {
            int hitAmount = weapon.attackPower;
            this.health -= hitAmount;
            return hitAmount;
        }

        public int ComputerChooseAttackType(Robot targetRobot, Herd herd, Random rng)
        {
            int hitAmount = 0;
            int attackChoice = 0;
            // Logic chain for Computer Choosing Attack target.
            // 0 = Scratch, 1 = Bite 2 = Tail Whip
            if (targetRobot.health < 50) // target health < 50, 50% chance to try and Bite, finishing off Robot
            {
                if (rng.Next() % 2 == 0)
                {
                    attackChoice = 1;
                }
            }
            else if (targetRobot.powerLevel == 0) // Tail Whip 100 hit rate
            {
                attackChoice = 2;
            }
            else if (health > 60)
            {
                attackChoice = 0;
            }
            else
            {
                // Default is random choice. TODO set more rules.
                attackChoice = rng.Next(0, 3);
            }
            // Determin if hit is successfull. hit amount stays 0 if fails.
            if (rng.Next(101) < attackTypeModifierHitChance[attackChoice])
            {
                hitAmount = (int)(attackPower * attackTypesModifiers[attackChoice]);
            }
            return targetRobot.GetHit(hitAmount);
        }
    }
}

