using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supreme_Commander_Thorn
{
    public class SolarBatteryHeater : UsableObject, IElectricDevice, IClimateModifier
    {
        #region Variables
        public float TemperatureModValue, EnergyStorageCapacity, CurrentEnergy;
        public bool IsCharging, IsRunning;
        float IElectricDevice.EnergyStorageCapacity { get => EnergyStorageCapacity; set => EnergyStorageCapacity = value; }
        float IElectricDevice.CurrentEnergy { get => CurrentEnergy; set => CurrentEnergy = value; }
        bool IElectricDevice.IsRunning { get => IsRunning; set => IsRunning = value; }
        bool IElectricDevice.IsCharging { get => IsCharging; set => IsCharging = value; }
        float IClimateModifier.AirConditionModValue { get => 0; set => _ = value; }
        float IClimateModifier.TemperatureModValue { get => TemperatureModValue; set => TemperatureModValue = value;}
        float IClimateModifier.RadiationModValue { get => 0; set => _ = value; }
        #endregion

        #region Constructors
        public SolarBatteryHeater() : base() { IsRunning = false; IsCharging = false; }
        public SolarBatteryHeater(string name, string mainDirectory, Vector2 pos, Vector2 dims, float temperatureMod, float maxEnergy) : base(name, mainDirectory, pos, dims)
        {
            this.TemperatureModValue = temperatureMod;
            this.EnergyStorageCapacity = maxEnergy;
            this.CurrentEnergy = maxEnergy;
            IsRunning = false;
            IsCharging = false;
        }
        #endregion

        #region Methods
        public override void ShowOptions(Object info)
        {
            List<InterfaceButton> buttons = new List<InterfaceButton>();
            buttons.Add(new InterfaceButton("Inspect", new Vector2(1615, 120), 230, ShowInspectWindow, null));
            String prompt1 = "Take To The Sun";
            if (IsCharging)
                prompt1 = "Take Back Home";
            buttons.Add(new InterfaceButton(prompt1, new Vector2(1615, 160), 230, ChangeCharging, null));
            if(!IsCharging)
            {
                String prompt2 = "Turn On";
                if (IsRunning)
                    prompt2 = "Turn Off";
                buttons.Add(new InterfaceButton(prompt2, new Vector2(1615, 200), 230, (Object info) => {
                    this.IsRunning = !this.IsRunning; NestedGui.ClearAdditionalOptions();}, null));
            }
            NestedGui.ShowInterface();
            NestedGui.ShowAdditionalOptions(buttons);
        }
        public void ChangeCharging(Object info)
        {
            if(!IsRunning && !IsCharging)
            {
                IsCharging = true;
                NestedGui.ClearAdditionalOptions();
            }
            else if (IsCharging)
            {
                IsCharging = false;
                NestedGui.ClearAdditionalOptions();
            }
        }
        public override void Update()
        {
            if(IsRunning)
            {
                this.CurrentEnergy -= 0.02f;
                if(this.CurrentEnergy <= 0)
                {
                    this.CurrentEnergy = 0;
                    IsRunning = false;
                }
            }
            if(IsCharging && Universe.IsItDay())
            {
                CurrentEnergy += 0.03f;
                if(CurrentEnergy> EnergyStorageCapacity)
                    CurrentEnergy = EnergyStorageCapacity;
            }
        }
        #endregion
    }
}
