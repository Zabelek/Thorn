using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class GameSubsystems
    {
        public static ClimateController NainPlanetClimateController;

        public static void Update()
        {
            NainPlanetClimateController.Update();
        }
    }
}
