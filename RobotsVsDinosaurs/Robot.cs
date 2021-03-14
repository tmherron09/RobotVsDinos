using System;
using System.Collections.Generic;

namespace RobotsVsDinosaurs
{
    public class Robot : Warrior
    {
        public Weapon weapon;
        public List<Weapon> weaponList;

        public Robot()
        {

        }
        public Robot(List<Weapon> weaponList, string name, int health, int powerLevel, int maxPower, int attackPower) : base( name,  health,  powerLevel,  maxPower, attackPower)
        {
            this.weaponList = weaponList;
        }

        public override int WarriorAttack(Warrior targetWarrior)
        {
            throw new NotImplementedException();
        }

        #region Weapon Methods
        public override void InitializeWarriors(Army fleet, Battlefield battlefield)
        {
            if (weapon == null && fleet.IsHuman)
            {
                ChooseWeapon(battlefield);
            }
            else if (weapon == null) // Depreciated Failsafe
            {
                foreach (Robot robot in fleet.Warriors)
                {
                    robot.ComputerChooseWeapon();
                }
            }
        }
        private void ChooseWeapon(Battlefield battlefield)
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
                    battlefield.UpdateStatsDisplay();
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



    }
}
