using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Robot
    {
        public string name;
        public int health;
        public int powerLevel; //stamina
        public int maxPowerLevel;
        public Weapon weapon;
        public List<Weapon> weaponList;

        public Robot(string name, List<Weapon> weaponList, int health, int powerLevel, int maxPowerLevel)
        {
            this.name = name;
            this.weaponList = weaponList;
            this.health = health;
            this.powerLevel = powerLevel;
            this.maxPowerLevel = maxPowerLevel;
    }

        #region Weapon Methods
        public void InitializeWeapon(Fleet fleet, Battlefield battlefield)
        {
            if (weapon == null && fleet.isHuman)
            {
                ChooseWeapon(battlefield);
            }
            else if (weapon == null)
            {
                // Should not be called: Assume if it has no weapon, all have no weapon. Initialize all.
                fleet.ComputerChooseAllWeapons();
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
            weaponList.Remove(this.weapon);
        }
        #endregion

        #region Attack/GetHit Methods
        public int Attack(Dinosaur targetDinosaur)
        {
            return targetDinosaur.GetHit(this.weapon);

        }
        public int GetHit(int attackPower)
        {
            int hitAmount = attackPower;
            health -= hitAmount;
            return hitAmount;
        }
        #endregion

    }
}
