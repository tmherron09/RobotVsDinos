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
        //List<DinosaurAttack> dinosaurAttacks;

        public Herd()
        {
            dinosaurs = new List<Dinosaur>();
            dinosaurTypes = new List<string> { "Troodon", "Quaesitosaurus", "T-Rex" };
        }

        public void InitializeHerd()
        {
            InitializeNewDinosaurList();
        }
        private void InitializeNewDinosaurList()
        {
            for (int i = 0; i < 3; i++)
            {
                dinosaurs.Add(new Dinosaur(dinosaurTypes[i]));
            }
        }


    }
}
