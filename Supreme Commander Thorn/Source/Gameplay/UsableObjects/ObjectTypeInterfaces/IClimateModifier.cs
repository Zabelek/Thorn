using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public interface IClimateModifier
    {
        public float AirConditionModValue { get; protected set; }
        public float TemperatureModValue { get; protected set; }
        public float RadiationModValue { get; protected set; }
    }
}
