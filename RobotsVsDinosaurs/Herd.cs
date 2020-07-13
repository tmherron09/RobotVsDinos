using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Herd
    {
        public List<Dinosaur> dinosaurs;
        public List<string> dinosaurTypes;
        public bool isHuman;
        //List<DinosaurAttack> dinosaurAttacks;

        public Herd()
        {
            dinosaurs = new List<Dinosaur>();
            dinosaurTypes = new List<string> { "Troodon", "Quaesitosaurus", "T-Rex" };
            isHuman = false;
        }

        public void InitializeHerd(Random rng)
        {
            InitializeNewDinosaurList(rng);
        }
        private void InitializeNewDinosaurList(Random rng)
        {
            for (int i = 0; i < 3; i++)
            {
                dinosaurs.Add(new Dinosaur(dinosaurTypes[i], rng));
            }
        }
        public Dinosaur ChooseDinosaurToFight()
        {
                Console.WriteLine("Please select which robot to use: ");
                for (int i = 0; i < dinosaurs.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {dinosaurs[i].typename} Attack Power: {dinosaurs[i].attackPower}");
                }
                int selection;
                bool valid = false;
                do
                {
                    valid = Int32.TryParse(Console.ReadLine(), out selection);
                    if (valid)
                    {
                        valid = (selection > 0 && selection <= dinosaurs.Count);
                        selection--;
                    }
                } while (!valid);
                return dinosaurs[selection];
        }
        public bool CheckHasDied(Dinosaur dinosaur)
        {
            return dinosaur.health <= 0;
        }
        public void RemoveDinosaur(Dinosaur dinosaur)
        {
            if(dinosaurs.Contains(dinosaur))
            {
                dinosaurs.Remove(dinosaur);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

    }
}
