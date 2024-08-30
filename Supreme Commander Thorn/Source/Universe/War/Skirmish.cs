using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class Skirmish
    {
        public Location Location;
        public int NumberOfEnemies, NumberOfAllies, MapSize;
        public Skirmish(Location location, int numberOfEnemies, int numberOfAllies, int mapSize)
        {
            this.Location = location;
            this.NumberOfEnemies = numberOfEnemies;
            this.NumberOfAllies = numberOfAllies;
            this.MapSize = mapSize;
        }
    }
}
