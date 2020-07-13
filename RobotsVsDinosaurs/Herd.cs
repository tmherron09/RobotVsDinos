using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Herd
    {
        List<Dinosaurs> dinosaurs;
        List<string> dinosaurTypes;
        //List<DinosaurAttack> dinosaurAttacks;

        public Herd()
        {
            dinosaurs = new List<Dinosaurs>();
            dinosaurTypes = new List<string> { "Troodon", "Quaesitosaurus", "T-Rex" };
        }

        private List<Dinosaurs> InitializeHerd()
        {
            throw new NotImplementedException();
            //List<Dinosaur> dinosaurs;
            //Dinosaur dinosaur = InitializeNewDinosaur();
            //dinosaurs.Add(dinosaur) 
            // times 3
            //return dinosaurs;
        }
        private Dinosaur InitializeNewDinosaur()
        {
            throw new NotImplementedException();
        }

    }
}
