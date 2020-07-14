using System;
using System.Collections.Generic;
using System.Linq;
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

        public Robot(string name, List<Weapon> weaponList)
        {
            this.name = name;
            this.weaponList = weaponList;
            health = 100;
            powerLevel = 20;
            maxPowerLevel = 20;
    }

        public void InitializeWeapon(Fleet fleet)
        {
            if(weapon == null && fleet.isHuman)
            {
                ChooseWeapon();
            }
            else if(weapon == null)
            {
                ComputerChooseWeapon();
            }
        }

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
        private void ChooseWeapon()
        {
            for(int i = 0; i < weaponList.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {weaponList[i].name} Attack Power: {weaponList[i].attackPower}");
            }
            int weaponChoice;
            bool valid = false;
            do
            {
                valid = Int32.TryParse(Console.ReadLine(), out weaponChoice);
                if (valid)
                {
                    valid = (weaponChoice > 0 && weaponChoice <= weaponList.Count);
                    weaponChoice--;
                }
            } while (!valid);
            this.weapon = weaponList[weaponChoice];
            weaponList.Remove(this.weapon);
        }
        public void ComputerChooseWeapon()
        {
            int mostAttackPower = 0;
            Weapon weaponChoice = weapon;
            foreach(Weapon weapon in weaponList)
            {
                if(weapon.attackPower > mostAttackPower)
                {
                    weaponChoice = weapon;
                    mostAttackPower = weapon.attackPower;
                }
            }
            this.weapon = weaponChoice;
            weaponList.Remove(this.weapon);
        }

    }
}
