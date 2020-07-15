using System;

namespace RobotsVsDinosaurs
{
    class Dinosaur : Warrior
    {

        public string[] attackTypes;
        public double[] attackTypesModifiers;
        public int[] attackTypeModifierHitChance;

        public Dinosaur(string name, int health, int powerLevel, int maxPowerLevel, int attackPower) : base(name, health, powerLevel, maxPowerLevel, attackPower)
        {

        }

        public void SetAttackTypes()
        {
            attackTypes = new string[] { "Scratch", "Bite", "Tail Whip" };
            attackTypesModifiers = new double[] { 1.25, 2.00, 1.00 };
            attackTypeModifierHitChance = new int[] { 80, 50, 100 };
        }

        #region Attack/ Attack Type Methods
        // Human attack consisting of choosing attack type. Calls GetHit method on target robot.
        public int HumanAttack(Robot targetRobot, Random rng, Battlefield battlefield)
        {
            // Set to 0 in case of miss.
            int hitAmount = 0;
            int selection;
            bool valid = false;
            // Verify input is valid before continuing.
            do
            {
                Console.WriteLine("Choose an Attack: ");
                for (int i = 0; i < attackTypes.Length; i++)
                {
                    Console.WriteLine($"{i + 1}) {attackTypes[i]} Hit Chance: {attackTypeModifierHitChance[i]}%  Bonus: x{attackTypesModifiers[i]} damage");
                }
                valid = Int32.TryParse(Console.ReadLine(), out selection);
                if (valid)
                {
                    valid = (selection > 0 && selection <= attackTypes.Length);
                    selection--;
                }
                if (!valid)
                {
                    battlefield.UpdateStatsDisplay();
                }
            } while (!valid);
            // Calculate if the attack hits based on attack type hit chace.
            if (rng.Next(101) < attackTypeModifierHitChance[selection])
            {
                // Calculate damage based on attack type modifier bonus.
                hitAmount = (int)(attackPower * attackTypesModifiers[selection]);
            }
            return targetRobot.GetHit(hitAmount);
        }
        // Computer log for deciding on attack type to use, then calculate damage.
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
        #endregion
        // Takes a robot's weapon as input to calculate damage. Returns value back to caller.
        //public int GetHit(Weapon weapon)
        //{
        //    int hitAmount = weapon.attackPower;
        //    this.health -= hitAmount;
        //    return hitAmount;
        //}

    }
}

