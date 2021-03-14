using System;

namespace RobotsVsDinosaurs
{
    public class Dinosaur : Warrior
    {

        public string[] attackTypes;
        public float[] attackTypesModifiers;
        public float[] attackTypeModifierHitChance;

        public Dinosaur()
        {
            SetAttackTypes();
        }
        public Dinosaur(string name, int health, int powerLevel, int maxPowerLevel, int attackPower) : base(name, health, powerLevel, maxPowerLevel, attackPower)
        {
            SetAttackTypes();
        }

        public void SetAttackTypes()
        {
            attackTypes = new string[] { "Scratch", "Bite", "Tail Whip" };
            attackTypesModifiers = new float[] { 1.25f, 2.00f, 1.00f };
            attackTypeModifierHitChance = new float[] { 80f, 50f, 100f };
        }

        public override int WarriorAttack(Warrior targetWarrior)
        {
            throw new NotImplementedException();
        }


        #region Attack/ Attack Type Methods
        // Human attack consisting of choosing attack type. Calls GetHit method on target robot.
        public override float HumanChooseAttackType(Warrior targetWarrior, Random rng, Battlefield battlefield)
        {
            // Set to 0 in case of miss.
            float hitAmount = 0f;
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
                hitAmount = AttackPower * attackTypesModifiers[selection];
            }
            return targetWarrior.GetHit(this, hitAmount);
        }
        // Computer log for deciding on attack type to use, then calculate damage.
        public override float ComputerChooseAttackType(Warrior targetRobot, Herd herd, Random rng)
        {
            int hitAmount = 0;
            int attackChoice = 0;
            // Logic chain for Computer Choosing Attack target.
            // 0 = Scratch, 1 = Bite 2 = Tail Whip
            if (targetRobot.Health < 50) // target health < 50, 50% chance to try and Bite, finishing off Robot
            {
                if (rng.Next() % 2 == 0)
                {
                    attackChoice = 1;
                }
            }
            else if (targetRobot.stamina == 0) // Tail Whip 100 hit rate
            {
                attackChoice = 2;
            }
            else if (Health > 60)
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
                hitAmount = (int)(AttackPower * attackTypesModifiers[attackChoice]);
            }
            return targetRobot.GetHit(this, hitAmount);
        }


        #endregion


        public override void InitializeWarriors(Army fleet, Battlefield battlefield)
        {
            // TODO: Add Logic to choose Dinosaurs.
        }

    }
}

