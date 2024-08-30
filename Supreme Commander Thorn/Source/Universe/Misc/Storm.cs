using System;
using System.Security.Policy;

namespace Supreme_Commander_Thorn
{
    public class Storm
    {
        #region Variables
        public DateTime StartDate, EndDate;
        //strength can be value from 1 to 5, it will be multiplied by 5 and this will be drop of temperature during it.
        public int Strength;
        #endregion

        #region Constructors
        public Storm(){}

        public Storm(DateTime startDate, DateTime endDate, int strength)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Strength = strength;
        }
        #endregion

        #region Methods
        internal void DecreaseLocationTemperature(Location location)
        {
            location.CurrentTemperature -= (float)this.Strength / 10;
            if(Universe.IsItDay())
            {
                if (location.CurrentTemperature < location.TemperatureDownDay-this.Strength*5)
                {
                    location.CurrentTemperature += (float)this.Strength / 10;
                }
            }
            else
            {
                if (location.CurrentTemperature < location.TemperatureDownNight - this.Strength * 7)
                {
                    location.CurrentTemperature += (float)this.Strength / 10;
                }
            }

        }
        #endregion
    }
}