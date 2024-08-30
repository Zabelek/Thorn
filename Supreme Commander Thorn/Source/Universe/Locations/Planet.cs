using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Supreme_Commander_Thorn
{
    public class Planet : ILocationContainer
    {
        #region Variables
        [XmlIgnore]
        public List<Location> MainLocations = new();
        [XmlIgnore]
        public String MainDirectory;
        public enum PlanetType { Asteroid, RockyPlanet, RockyPlanetNoAtmosphere, GasGiant }
        public String Name;
        public PlanetType Type;
        //1 is the size of Earth.
        public float Size;
        //radiation level from 0 to 100. 10 is a value that requires to go in a special suit.
        //AirQuality has to be above 70 to make it possible to breathe without additional equipment.
        public float AverageTemperature, RadioationLevel, AirQuality;
        #endregion

        #region Constructors
        public Planet() {}
        public Planet(string name)
        {
            this.Name = name;
        }
        public Planet(string name, int averageTemperature, int radioationLevel, int airQuality)
        {
            this.Name = name;
            this.AverageTemperature = averageTemperature;
            this.RadioationLevel = radioationLevel;
            this.AirQuality = airQuality;
        }
        #endregion

        #region Methods
        public void AddLocation(Location location)
        {
            this.MainLocations.Add(location);
            location.IsOnThePlanet = true;
            location.ParentLocation = this;
            location.UpdateAllClimateData(this);
        }
        public void RemoveLocation(Location locatiom)
        {
            this.MainLocations.Remove(locatiom);
        }
        public void ClearLocations()
        {
            this.MainLocations.Clear();
        }
        public Planet GetPlanet()
        {
            return null;
        }
        public bool IsPlanet()
        {
            return true;
        }
        #endregion
    }
}