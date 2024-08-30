using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public interface IElectricDevice
    {
        public float EnergyStorageCapacity { get; set; }
        public float CurrentEnergy { get; set; }
        public bool IsRunning { get; set; }
        public bool IsCharging { get; set; }
    }
}
