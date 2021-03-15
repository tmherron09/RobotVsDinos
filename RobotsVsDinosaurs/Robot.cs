using System;
using System.Collections.Generic;

namespace RobotsVsDinosaurs
{
    public class Robot : Warrior
    {
        public Weapon weapon;
        public List<Weapon> weaponList;

        public override bool IsInitialized { get
            {
                return weapon != null;
            } 
        }


        public Robot()
        {

        }
        public Robot(Army army, ref List<Weapon> weaponList, string name, float health, float powerLevel, float maxPower, float baseAttackPower) : base(army,  name,  health,  powerLevel,  maxPower, baseAttackPower)
        {
            this.weaponList = weaponList;
        }

        public override int WarriorAttack(Warrior targetWarrior)
        {
            throw new NotImplementedException();
        }

        #region Weapon Methods

        // TODO: Move Weapon Initialization to First Stage of Robot Attack.
        public override void InitializeWarriors()
        {
            if (weapon == null)
            {
                ChooseWeapon();
            }
        }


        private void ChooseWeapon()
        {
            int weaponChoice;
            bool valid = false;
            do
            {
                for (int i = 0; i < weaponList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) Name: {weaponList[i].name} | Attack Power: {weaponList[i].attackPower}");
                }
                valid = Int32.TryParse(Console.ReadLine(), out weaponChoice);
                if (valid)
                {
                    valid = (weaponChoice > 0 && weaponChoice <= weaponList.Count);
                    weaponChoice--;
                }
                if (!valid)
                {
                    // TODO: Replace with small line clear method.
                    //UpdateStatsDisplay();
                }
            } while (!valid);
            this.weapon = weaponList[weaponChoice];
            this.AttackPower = weapon.attackPower;
            weaponList.Remove(this.weapon);
        }
        public void ComputerChooseWeapon()
        {
            int mostAttackPower = 0;
            Weapon weaponChoice = weapon;
            foreach (Weapon weapon in weaponList)
            {
                if (weapon.attackPower > mostAttackPower)
                {
                    weaponChoice = weapon;
                    mostAttackPower = weapon.attackPower;
                }
            }
            this.weapon = weaponChoice;
            this.AttackPower = weapon.attackPower;
            weaponList.Remove(this.weapon);
        }

        #endregion



        public override float HumanChooseAttackType(Warrior warrior, Random rng, Battlefield currentBattlefield)
        {
            throw new NotImplementedException();
        }

        public override float ComputerChooseAttackType(Warrior warrior, Herd herd, Random rng)
        {
            throw new NotImplementedException();
        }


        public override string ChoiceDisplayMessage(int? indexOf = null)
        {
            string SelectionNumber = indexOf != null ? $"{indexOf + 1}) " : "";
            return $"{SelectionNumber}Name: {Name} | Weapon: {(weapon != null ? weapon.name : "No Weapon Selected.")}";
        }

    }
}
