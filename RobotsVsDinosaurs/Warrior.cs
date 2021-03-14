using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    public abstract class Warrior
    {
        private float _health;
        private float _powerLevel;
        private float _attackPower;

        public string Name { get; set; }
        public float Health
        {
            get
            {


                return _health > 0 ? _health : 0f;
            }
            private set
            {
                _health = value;
            }

        }

        public int stamina;
        public int maxPowerLevel;

        public float AttackPower { get; set; }


        public Warrior()
        {
            // Create null as reference
        }
        public Warrior(string name, int health, int powerLevel, int maxPowerLevel, float attackPower)
        {
            this.Name = name;
            this.Health = health;
            this.stamina = powerLevel; // stamina
            this.maxPowerLevel = maxPowerLevel;
            this.AttackPower = attackPower;
        }

        public abstract float HumanChooseAttackType(Warrior warrior, Random rng, Battlefield currentBattlefield);
        public abstract float ComputerChooseAttackType(Warrior warrior, Herd herd, Random rng);
        public abstract int WarriorAttack(Warrior targetWarrior);


        public float Attack(Warrior targetWarrior)
        {
            return targetWarrior.GetHit(this, AttackPower);
        }
        public float GetHit(Warrior attackingWarrior, float attackHitAmount)
        {
            Health -= attackHitAmount;
            return attackHitAmount;
        }

        public abstract void InitializeWarriors(Army fleet, Battlefield battlefield);

    }
}
