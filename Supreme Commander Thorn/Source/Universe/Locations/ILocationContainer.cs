using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public interface ILocationContainer
    {
        public void AddLocation(Location locatiom);
        public void RemoveLocation(Location locatiom);
        public void ClearLocations();
        public Planet GetPlanet();
        public bool IsPlanet();
    }
}
