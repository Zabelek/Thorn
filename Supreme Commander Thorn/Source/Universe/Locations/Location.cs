using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Supreme_Commander_Thorn
{
    public class Location : ILocationContainer
    {
        #region Variables
        [XmlIgnore]
        private List<Person> _people = new();
        [XmlIgnore]
        public List<Location> Sublocations = new();
        [XmlIgnore]
        public List<UsableObject> Usables = new();
        [XmlIgnore]
        public ILocationContainer ParentLocation;
        [XmlIgnore]
        public String MainDirectory;
        [XmlIgnore]
        public float TemperatureDownDay, TemperatureDownNight, TemperatureUpDay, TemperatureUpNight;
        public String Name;
        //short name max 22 chars
        public String ShortName;
        public bool IsOnThePlanet, IsOpenSpace;
        public bool CustomClimate;
        public float AverageTemperature, CurrentTemperature, RadioationLevel, AirQuality, TemperatureResistance;
        #endregion

        #region Constructors
        public Location() 
        {
            CustomClimate = false;
            TemperatureResistance = 1;
        }
        public Location(string name)
        {
            this.Name = name;
            this.IsOnThePlanet = false;
            this.IsOpenSpace = true;
            CustomClimate = false;
            TemperatureResistance = 1;
        }
        #endregion

        #region Methods
        public void UpdateAllClimateData(float averageTemperature, float radioationLevel, float airQuality)
        {
            if(!CustomClimate)
            {
                if(IsOpenSpace)
                {
                    this.AverageTemperature = averageTemperature;
                    this.RadioationLevel = radioationLevel;
                    this.AirQuality = airQuality;
                    CurrentTemperature = this.AverageTemperature;
                }
            }
            TemperatureDownDay = this.AverageTemperature - 5;
            TemperatureDownNight = this.AverageTemperature - 10;
            TemperatureUpDay = this.AverageTemperature + 5;
            TemperatureUpNight = this.AverageTemperature;
            foreach (Location loc in this.Sublocations)
            {
                loc.UpdateAllClimateData(this.AverageTemperature, this.RadioationLevel, this.AirQuality);
                loc.IsOnThePlanet= this.IsOnThePlanet;
            }
        }
        public void UpdateAllClimateData(Planet planet)
        {
            this.IsOnThePlanet = true;
            UpdateAllClimateData(planet.AverageTemperature, planet.RadioationLevel, planet.AirQuality);
        }
        public void AddLocation(Location locatiom)
        {
            this.Sublocations.Add(locatiom);
            locatiom.IsOnThePlanet = this.IsOnThePlanet;
            locatiom.ParentLocation = this;
            locatiom.UpdateAllClimateData(AverageTemperature, RadioationLevel, AirQuality);

        }
        public void RemoveLocation(Location locatiom)
        {
            this.Sublocations.Remove(locatiom);
        }
        public void ClearLocations()
        {
            this.Sublocations.Clear();
        }
        public void AddPerson(Person person)
        {

            person.CurrentLocation?.RemovePerson(person);
            person.CurrentLocation = this;
            this._people.Add(person);
        }
        public void RemovePerson(Person person)
        {
            this._people.Remove(person);
        }
        public bool HasPeople()
        {
            return this._people.Count > 0;
        }

        public List<Person> GetPeople()
        {
            return this._people;
        }

        public string GetImagePath()
        {
            if (Universe.IsItDay())
                return MainDirectory + "\\Image_Day.png";
            else return MainDirectory + "\\Image_Night.png";
        }
        public string GetImagePathDayOnly()
        {
                return MainDirectory + "\\Image_Day.png";
        }
        public String GetShortName()
        {
            if(ShortName!= null) return ShortName;
            else if(Name.Length<22) return Name;
            else
            {
                String temp = Name.Substring(0, 20);
                temp += "...";
                return temp;
            }
        }
        public Planet GetPlanet()
        {
            if (ParentLocation != null)
            {
                if (ParentLocation.IsPlanet())
                    return (Planet)ParentLocation;
                else return ParentLocation.GetPlanet();
            }
            else return null;
        }
        public bool IsPlanet()
        {
            return false;
        }
        #endregion
    }
}