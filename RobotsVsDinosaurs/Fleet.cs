using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsVsDinosaurs
{
    class Fleet
    {
        List<Robot> robots;
        List<Weapon> availableWeapons;

        public Fleet()
        {
            robots = InitializeFleet();
            availableWeapons = new List<Weapon>();
        }

        private List<Robot> InitializeFleet()
        {
            throw new NotImplementedException();
            //List<Robot> robots;
            //Robot robot = InitializeNewRobot();
            //robots.Add(robot) 
            // times 3
            //return robots;
        }
        private Robot InitializeNewRobot()
        {
            Robot newRobot = new Robot(availableWeapons);
        }
    }
}
