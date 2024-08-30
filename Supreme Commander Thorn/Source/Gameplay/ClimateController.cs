using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Supreme_Commander_Thorn
{
    public class ClimateController
    {
        #region Variables
        [XmlIgnore]
        private Random _random;
        [XmlIgnore]
        private BasicTimer _timer;
        [XmlIgnore]
        public bool IsStormRunning;
        [XmlIgnore]
        public Planet ControlledPlanet { get; set; }
        public float CurrentTendency;
        public Storm Storm;
        #endregion

        #region Constructors
        public ClimateController() {
            _timer = new BasicTimer(0);
            _timer.MSec = 5000;
            _random = new Random();
            CurrentTendency = 0;
            IsStormRunning = false;
        }
        #endregion

        #region Methods
        public void ProceedTick()
        {
            foreach (Location mainLocation in ControlledPlanet?.MainLocations)
                UpdateTemperature(mainLocation);
        }

        private void UpdateTemperature(Location location)
        {
            float newtemperature = location.CurrentTemperature;
            if (CurrentTendency == 0)
            {
                newtemperature += ((float)((_random.Next() % 3) - 1)) / 10;
                if (newtemperature < location.CurrentTemperature)
                    CurrentTendency = -1;
                else if (newtemperature > location.CurrentTemperature)
                    CurrentTendency = 1;
            }
            else
            {
                newtemperature += (((float)((_random.Next() % 3) - 1)) / 10)+CurrentTendency/20;
                if (_random.Next()%40 == 5)
                    CurrentTendency = -CurrentTendency;
            }
            
            if(Universe.IsItDay())
            {
                if (newtemperature < location.TemperatureDownDay)
                    newtemperature+=0.1f;
                else if(newtemperature > location.TemperatureUpDay)
                    newtemperature -= 0.1f;
            }
            else
            {
                if (newtemperature < location.TemperatureDownNight)
                    newtemperature += 0.1f;
                else if (newtemperature > location.TemperatureUpNight)
                    newtemperature -= 0.1f;
            }
            location.CurrentTemperature = newtemperature;
            if (Storm?.StartDate < Universe.GameDate)
            {
                Storm.DecreaseLocationTemperature(location);
                IsStormRunning = true;
            }
            if (location.IsOpenSpace==true && location.Sublocations.Count>0)
            {
                foreach(Location subLocation in location.Sublocations)
                    UpdateTemperature(subLocation, newtemperature);
            }
        }
        private void UpdateTemperature(Location location, float newTemperature)
        {
            if (location.IsOpenSpace == true)
                location.CurrentTemperature = newTemperature;
            else
                UpdateTemperatureForInterior(location, newTemperature);
            if(location.Sublocations.Count>0)
                foreach(Location subloc in location.Sublocations)
                        UpdateTemperature(subloc, newTemperature);
        }

        private void UpdateTemperatureForInterior(Location location, float outsideTemperature)
        {
            float tempDifference = Math.Abs(location.CurrentTemperature - outsideTemperature);
            float tempMod = (tempDifference - (tempDifference * location.TemperatureResistance))/100;
            if (location.CurrentTemperature < outsideTemperature)
                location.CurrentTemperature += tempMod;
            else location.CurrentTemperature -= tempMod;
            foreach(UsableObject obj in location.Usables)
            {
                if(obj is IClimateModifier)
                    if(((IClimateModifier)obj).TemperatureModValue != 0)
                        if (obj is IElectricDevice && ((IElectricDevice)obj).IsRunning == false) {}
                        else location.CurrentTemperature += ((IClimateModifier)obj).TemperatureModValue;
            }
        }

        public void Update()
        {
            _timer.UpdateTimer();
            if(_timer.Test())
            {
                _timer.Reset();
                if(ControlledPlanet!=null)
                    ProceedTick();
                ScheduleStorm();
            }
        }

        private void ScheduleStorm()
        {
            if(_random.Next()%500 == 0 && Storm == null)
            {
                var startDate = Universe.GameDate.AddHours((_random.Next() % 7)+5);
                var endDate = startDate.AddHours((_random.Next() % 4) + 3);
                Storm = new Storm(startDate, endDate, _random.Next()%5);
            }
            if (Storm?.EndDate < Universe.GameDate)
            {
                Storm = null;
                IsStormRunning = false;
            }
        }
        #endregion
    }
}
