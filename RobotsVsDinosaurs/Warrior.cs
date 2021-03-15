using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    public abstract class Warrior
    {
        private Army WarriorArmy;

        private float _health;
        private float _powerLevel;
        private float _attackPower;
        private float _stamina;

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

        public float Stamina { get
            {
                return _stamina;
            }
            set
            {
                // TODO: refact to private call.
                _stamina = value;
            }
        }
        public float maxPowerLevel;

        public float AttackPower { get; set; }
        public abstract bool IsInitialized { get; }


        public Warrior()
        {
            // Create null as reference
        }
        public Warrior(Army army, string name, float health, float powerLevel, float maxPowerLevel, float baseAttackPower)
        {
            WarriorArmy = army;
            this.Name = name;
            this.Health = health;
            this.Stamina = powerLevel; // stamina
            this.maxPowerLevel = maxPowerLevel;
            this.AttackPower = baseAttackPower;
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

        public abstract void InitializeWarriors();

        public abstract string ChoiceDisplayMessage(int? indexOf = null);
    }
}
