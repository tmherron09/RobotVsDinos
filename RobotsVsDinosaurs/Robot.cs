using System;
using System.Collections.Generic;

namespace RobotsVsDinosaurs
{
    class Robot : Warrior
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

        #region Weapon Methods
        public override void InitializeWeapon(Army fleet, Battlefield battlefield)
        {
            if (weapon == null && fleet.isHuman)
            {
                ChooseWeapon(battlefield);
            }
            else if (weapon == null) // Depreciated Failsafe
            {
                foreach (Robot robot in fleet.warriors)
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
            this.attackPower = weapon.attackPower;
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
            this.attackPower = weapon.attackPower;
            weaponList.Remove(this.weapon);
        }
        #endregion



    }
}
